using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project> GetProjectByIdAsync(int id);
        bool Add(Project project);
        bool Update(Project project);
        bool Delete(Project project);
        bool Save();
    }
}
