using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class AgendaCommentsRepository : IAgendaCommentsRepository
    {

        private readonly AppDbContext _context;
        public AgendaCommentsRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(AgendaComments agendaComments)
        {
            _context.Add(agendaComments);
            return Save();
        }

        public bool Delete(AgendaComments agendaComments)
        {
            _context.Remove(agendaComments);
            return Save();
        }

        public AgendaComments GetById(int id)
        {
            return _context.AgendaComments.FirstOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<AgendaComments>> GetByProjectId(int id)
        {
            return await _context.AgendaComments.Where(u => u.ProjectId == id).OrderByDescending(u => u.Id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AgendaComments agendaComments)
        {
            _context.Update(agendaComments);
            return Save();
        }
    }
}
