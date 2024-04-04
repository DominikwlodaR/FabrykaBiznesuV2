using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class BudgetRepository: IBudgetRepository
    {
        private readonly AppDbContext _context;


        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }

        public Budget GetSumById(int id)
        {
            return _context.Budget.FirstOrDefault(p => p.id == id);
        }


        public bool Add(Budget item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(Budget item)
        {
            _context.Remove(item);
            return Save();
        }

        public bool Update(Budget item)
        {
            _context.Update(item);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Budget>> GetByProjectId(int id)
        {
            return await _context.Budget.Where(u => u.projectId == id).ToListAsync();
        }
    }
}
