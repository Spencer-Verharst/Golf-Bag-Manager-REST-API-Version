using Microsoft.AspNetCore.Mvc;

namespace GolfBagManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GolfBagController : ControllerBase
    {
        private readonly IGolfBag _golfBag;
        private readonly IClubFactory _clubFactory;

        // Constructor injection - ASP.NET Core handles this automatically
        public GolfBagController(IGolfBag golfBag, IClubFactory clubFactory)
        {
            _golfBag = golfBag;
            _clubFactory = clubFactory;
        }

        // GET: api/golfbag
        [HttpGet]
        public ActionResult<IEnumerable<Club>> GetAllClubs()
        {
            return Ok(_golfBag.GetAllClubs());
        }

        // GET: api/golfbag/count
        [HttpGet("count")]
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

        // POST: api/golfbag
        [HttpPost]
        public ActionResult<Club> AddClub([FromBody] AddClubRequest request)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Type) || string.IsNullOrWhiteSpace(request.Brand))
            {
                return BadRequest("Type and Brand are required");
            }

            if (request.Distance <= 0)
            {
                return BadRequest("Distance must be positive");
            }

            // Check if bag is full
            if (_golfBag.IsFull())
            {
                return BadRequest("Bag is full (14 clubs maximum)");
            }

            // Create the club
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

            // Check for duplicate
            if (_golfBag.FindClubInBag(club.Type))
            {
                return BadRequest($"A {club.Type} already exists in the bag");
            }

            // Add to bag
            if (_golfBag.AddClub(club))
            {
                return CreatedAtAction(nameof(GetAllClubs), club);
            }

            return BadRequest("Could not add club");
        }

        // DELETE: api/golfbag/{clubType}
        [HttpDelete("{clubType}")]
        public ActionResult RemoveClub(string clubType)
        {
            if (_golfBag.RemoveClub(clubType))
            {
                return NoContent(); // 204 No Content = success
            }

            return NotFound($"Club type '{clubType}' not found in bag");
        }

        // GET: api/golfbag/{clubType}/exists
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

