using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class AgendaTaskRepository : IAgendaTaskRepository
    {

        private readonly AppDbContext _context;

        public AgendaTaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(AgendaTask agendaTask)
        {
            _context.Add(agendaTask);
            return Save();
        }

        public bool Delete(AgendaTask agendaTask)
        {
            _context.Remove(agendaTask);
            return Save();  
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AgendaTask agendaTask)
        {
            _context.Update(agendaTask);
            return Save();  
        }

        public async Task<IEnumerable<AgendaTask>> GetByProjectId(int id)
        {
            return await _context.AgendaTask.Where(u => u.ProjectID == id).ToListAsync();
        }

        public AgendaTask GetTaskById(int id)
        {
            return _context.AgendaTask.FirstOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<AgendaTask>> GetAll()
        {
            return await _context.AgendaTask.ToListAsync();
        }

        public async Task<IEnumerable<AgendaTask>> GetByTeamId(int id)
        {
            return await _context.AgendaTask.Where(u => u.TeamId == id).ToListAsync();
        }
    }
}
