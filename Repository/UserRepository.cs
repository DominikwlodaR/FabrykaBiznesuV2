using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext Context { get; }

        public bool Add(AppUser user)
        {
           _context.Add(user);
            return Save();
        }

        public bool Delete(int id)
        {
           _context.Remove(id);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
           return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetByTeamId(int teamId)
        {
            return await _context.Users.Where(u => u.TeamID == teamId).ToListAsync();

        }


        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return  await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

    

        public bool Save()
        {
            var saved  = _context.SaveChanges(); 
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
