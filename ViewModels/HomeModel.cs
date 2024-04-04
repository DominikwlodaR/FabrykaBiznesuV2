using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class homeModel
    {
        public AppUser AppUser { get; set; }

        public List<Event> Events { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
