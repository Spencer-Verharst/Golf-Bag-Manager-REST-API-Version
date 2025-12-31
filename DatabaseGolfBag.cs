using Microsoft.EntityFrameworkCore;

namespace GolfBagManagerAPI
{
    public class DatabaseGolfBag : IGolfBag
    {
        private readonly GolfBagDbContext _context;
        private const int MAX_CLUBS = 14;

        public DatabaseGolfBag(GolfBagDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Club> GetAllClubs()
        {
            return _context.Clubs.ToList();
        }

        public int GetClubCount()
        {
            return _context.Clubs.Count();
        }

        public bool IsFull()
        {
            return GetClubCount() >= MAX_CLUBS;
        }

        public bool AddClub(Club club)
        {
            if (IsFull()) return false;

            _context.Clubs.Add(club);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveClub(string clubType)
        {
            var club = _context.Clubs
                .FirstOrDefault(c => c.Type.ToLower() == clubType.ToLower());

            if (club == null) return false;

            _context.Clubs.Remove(club);
            _context.SaveChanges();
            return true;
        }

        public bool FindClubInBag(string clubType)
        {
            return _context.Clubs
                .Any(c => c.Type.ToLower() == clubType.ToLower());
        }
    }
}
