using FabrykaBiznesuV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FabrykaBiznesuV2.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
       
        public DbSet<Project> Projects { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<AgendaTask> AgendaTask { get; set; }
        public DbSet<AgendaComments> AgendaComments { get; set; }
        public DbSet<Team> Team { get; set; }


    }
}
