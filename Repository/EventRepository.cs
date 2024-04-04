using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class EventRepository : IEvnetRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Event item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(Event item)
        {
            _context.Remove(item);
            return Save();
        }

        public bool Update(Event item)
        {
            _context.Update(item);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetByUserId(string id)
        {
            return await _context.Events.Where(u => u.UserID == id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FirstAsync(u => u.EventID.Equals(id));
        }
    }
}
