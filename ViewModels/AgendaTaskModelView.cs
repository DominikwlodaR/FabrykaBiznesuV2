using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class AgendaTaskModelView
    {
        public Project project { get; set; }
        public AgendaTask? agendaTask { get; set; }
        public List<AgendaTask> agendaTaskCollection { get; set; }
        public AgendaTask? agendaTaskToEdit { get; set; }
        public List<AgendaComments>? comments { get; set; }

    }
}
