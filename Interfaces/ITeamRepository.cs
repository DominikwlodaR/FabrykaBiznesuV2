using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAll();
        Task<Team> getTeamByID(int id);
        bool Add(Team team);
        bool Update(Team team);
        bool Delete(Team team);
        bool Save();
    }
}
