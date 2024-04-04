using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class Budget
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Project")]
        public int? projectId { get; set; }
        public string userId { get; set; }
        public string date { get; set; }
        public string? acceptDate { get; set; }
        public string? acceptUserID { get; set; }    
        public string status { get; set; }
        public string description { get; set; }
        public double sum { get; set; }

        public Project? Project { get; set; }

    }
}
