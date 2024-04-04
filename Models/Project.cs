using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabrykaBiznesuV2.Models
{
    public class Project
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Nie podano nazwy projektu")]
        [StringLength(30, ErrorMessage = "Nazwa projekru może zawierać max 30 znaków.")]
        public string projectName { get; set; }
        [Required(ErrorMessage = "Nie wybrano kierownika projektu")]
        [ForeignKey("AppUser")]
        public string projectManagerId { get; set; }
        [Required(ErrorMessage = "Nie wybrano przewodniczącego")]
        public string managementLiderId { get; set; }
        [Required(ErrorMessage = "Nie wybrano członka komitetu")]
        public string managementMemberId { get; set; }
        [Required(ErrorMessage = "Nie wybrano kategorii")]
        public string category { get; set; }
        [Required(ErrorMessage = "Nie podano daty początkowej")]
        [DataType(DataType.Date)]
        public DateTime? start { get; set; }
        [Required(ErrorMessage = "Nie podano daty końcowej")]

        [DataType(DataType.Date)]
        public DateTime? end { get; set; }
        [Required(ErrorMessage = "Nie wpisanu budżetu")]
        public double? ProjectBudget { get; set; }
        [Required(ErrorMessage = "Nie podano odchylenia")]
        public double? budgetDeviation { get; set; }
        [Required(ErrorMessage = "Pole nie wypełnione")]
        public string projectScope { get; set; }
        [Required(ErrorMessage = "Pole nie wypełnione")]
        public string products { get; set; }
        [Required(ErrorMessage = "Pole nie wypełnione")]
        public string businessGoals { get; set; }
        [Required(ErrorMessage = "Pole nie wypełnione")]
        public string costs { get; set; }

        public string status { get; set; }

        public AppUser? AppUser { get; set; }
        public ICollection<Budget>? Budgets { get; set; }
        public ICollection<AgendaTask>? AgendaTasks { get; set; }
        public ICollection<AgendaComments>? AgendaComments { get; set; }
    }
}
