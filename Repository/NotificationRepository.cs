using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext Context { get; }

        

        public bool Add(Notification notification)
        {
            _context.Add(notification);
            return Save();
        }

        public bool Delete(int id)
        {
            _context.Remove(id);
            return Save();
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetByUserId()
        {
            return await _context.Notifications.Where(u => u.UserID == "dcdb087f-0b8d-4a4f-ae78-91335c099b17").ToListAsync();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Notification user)
        {
            throw new NotImplementedException();
        }
    }
}
