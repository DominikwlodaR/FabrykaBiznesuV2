using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FabrykaBiznesuV2.Models
{
    public class AgendaTask
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string? Predecessor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Duration { get; set; }
        public int? Progress { get; set; }
        [JsonIgnore]
        public List<AgendaTask> SubTasks { get; set; }
        public bool isSubtask {  get; set; }
        public int ProjectID { get; set; }
        public bool IsManual { get; set; }
        public string Status { get; set; }
        public int TeamId { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }

    }
}
