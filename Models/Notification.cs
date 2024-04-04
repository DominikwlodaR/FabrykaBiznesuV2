using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class Notification
    {
        [Key]
        public int NotId { get; set; }
        [ForeignKey("AppUser")]
        public string? UserID { get; set; }
        public string? Status { get; set; }
        public string? Date { get; set; }
        public string? Message { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
