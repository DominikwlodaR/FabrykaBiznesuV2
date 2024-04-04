using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class AgendaTaskViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string? Predecessor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Progress { get; set; }
        public List<AgendaTask> SubTasks { get; set; }
        public bool isSubtask { get; set; }
        public int ProjectID { get; set; }
        public bool IsManual { get; set; }
        public string Status { get; set; }
        public int TeamId { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string ProjectName { get; set; }
    }
}
