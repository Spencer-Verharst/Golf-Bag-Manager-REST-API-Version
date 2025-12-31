namespace GolfBagManagerAPI
{
    public interface IGolfBag
    {
        IReadOnlyList<Club> GetAllClubs();
        int GetClubCount();
        bool IsFull();
        bool AddClub(Club club);
        bool RemoveClub(string clubType);
        bool FindClubInBag(string clubType);
    }
}
