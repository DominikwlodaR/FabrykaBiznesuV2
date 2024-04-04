using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class TeamViewModel
    {
        public AppUser user {  get; set; }
        public Team? team { get; set; }
        public List<AppUser> teamMembers { get; set; }
        public List<AgendaTaskViewModel>? teamTasks { get; set; }
        public List<Project>? projects { get; set; }


    }
}
