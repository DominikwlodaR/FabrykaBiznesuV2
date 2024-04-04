using FabrykaBiznesuV2.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FabrykaBiznesuV2.ViewModels
{
    public class CalendarViewModel
    {
        [ValidateNever]
        public DateTime CurrentDate { get; set; }
        [ValidateNever]
        public AppUser AppUser { get; set; }

        public Event Event { get; set; }
        [ValidateNever]
        public string StartTime { get; set; }
        [ValidateNever]
        public string EndTime { get; set; }
        [ValidateNever]
        public List<Event> Events { get; set; }
    }
}
