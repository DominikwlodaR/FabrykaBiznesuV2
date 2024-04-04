using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [ForeignKey("AppUser")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string UserID {  get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Start { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string End { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
