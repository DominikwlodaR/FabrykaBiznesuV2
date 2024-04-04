using System.ComponentModel.DataAnnotations;

namespace FabrykaBiznesuV2.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamLeaderId { get; set; }


   
        public ICollection<AppUser> Users { get; set; }
    }
}
