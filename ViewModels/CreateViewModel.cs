using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class CreateViewModel
    {
        public AppUser? user { get; set; }
        public Event? Event { get; set; }
        public Team? team { get; set; }
    }
}
