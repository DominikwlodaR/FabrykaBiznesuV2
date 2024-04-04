using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }

        public Team Team { get; set; }

        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
