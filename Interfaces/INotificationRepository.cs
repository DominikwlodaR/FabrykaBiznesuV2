using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAll();
        Task<IEnumerable<Notification>> GetByUserId();
        
        bool Add(Notification user);
        bool Update(Notification user);
        bool Delete(int id);
        bool Save();
    }
}
