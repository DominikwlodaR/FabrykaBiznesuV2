using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class ResourcesViewModel
    {
        public Project? project { get; set; }
        public List<AgendaTask>? tasks { get; set; }
        public List<Team>? teams { get; set; }
    }
}
