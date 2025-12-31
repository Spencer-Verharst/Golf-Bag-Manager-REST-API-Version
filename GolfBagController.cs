using Microsoft.AspNetCore.Mvc;

namespace GolfBagManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GolfBagController : ControllerBase
    {
        private readonly IGolfBag _golfBag;
        private readonly IClubFactory _clubFactory;

        public GolfBagController(IGolfBag golfBag, IClubFactory clubFactory)
        {
            _golfBag = golfBag;
            _clubFactory = clubFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Club>> GetAllClubs()
        {
            return Ok(_golfBag.GetAllClubs());
        }

        [HttpGet("Count")]
        public ActionResult<object> GetClubCount()
        {
            var count = _golfBag.GetClubCount();
            return Ok(new
            {
                count = count,
                maxClubs = 14,
                spotsRemaining = 14 - count
            });
        }

        [HttpPost("Club")]
        public ActionResult<Club> AddClub([FromBody] AddClubRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Type) || string.IsNullOrWhiteSpace(request.Brand))
            {
                return BadRequest("Type and Brand are required");
            }

            if (request.Distance <= 0)
            {
                return BadRequest("Distance must be positive");
            }

            if (_golfBag.IsFull())
            {
                return BadRequest("Bag is full (14 clubs maximum)");
            }

            var club = _clubFactory.CreateClub(
                request.Type,
                request.Brand,
                request.Distance,
                request.Number,
                request.WedgeType);

            if (club == null)
            {
                return BadRequest("Invalid club type");
            }

            if (_golfBag.FindClubInBag(club.Type))
            {
                return BadRequest($"A {club.Type} already exists in the bag");
            }

            if (_golfBag.AddClub(club))
            {
                return CreatedAtAction(nameof(GetAllClubs), club);
            }

            return BadRequest("Could not add club");
        }

        [HttpDelete("{clubType}")]
        public ActionResult RemoveClub(string clubType)
        {
            if (_golfBag.RemoveClub(clubType))
            {
                return NoContent();
            }

            return NotFound($"Club type '{clubType}' not found in bag");
        }

        [HttpGet("{clubType}/exists")]
        public ActionResult<object> CheckClubExists(string clubType)
        {
            var exists = _golfBag.FindClubInBag(clubType);
            return Ok(new { clubType, exists });
        }
    }

    // Request model
    public record AddClubRequest(
        string Type,
        string Brand,
        int Distance,
        int? Number = null,
        string? WedgeType = null);
}



