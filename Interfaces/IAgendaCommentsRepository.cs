using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IAgendaCommentsRepository
    {
        Task<IEnumerable<AgendaComments>> GetByProjectId(int id);
        AgendaComments GetById(int id);
        bool Add(AgendaComments agendaComments);
        bool Update(AgendaComments agendaComments);
        bool Delete(AgendaComments agendaComments);
        bool Save();
    }
}
