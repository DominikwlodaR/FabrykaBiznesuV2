using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface IEvnetRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<IEnumerable<Event>> GetByUserId(string id);
        Task<Event> GetEventByIdAsync(int id);
        bool Add(Event user);
        bool Update(Event item);
        bool Delete(Event item);
        bool Save();
    }
}
