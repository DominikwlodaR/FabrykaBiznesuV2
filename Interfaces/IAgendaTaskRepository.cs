using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IAgendaTaskRepository
    {
        Task<IEnumerable<AgendaTask>> GetByProjectId(int id);
        Task<IEnumerable<AgendaTask>> GetByTeamId(int id); 
        AgendaTask GetTaskById(int id);
        Task<IEnumerable<AgendaTask>> GetAll();
        bool Add(AgendaTask agendaTask);
        bool Update(AgendaTask agendaTask);
        bool Delete(AgendaTask agendaTask);
        bool Save();
    }
}
