using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();     
        Task<IEnumerable<AppUser>> GetByTeamId(int teamId);     
        Task<AppUser> GetUserByIdAsync(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(int id);    
        bool Save ();

    } 
}
