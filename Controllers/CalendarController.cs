using Microsoft.AspNetCore.Mvc;
using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Repository;
using System.Linq;
using System.Text.Json;

namespace FabrykaBiznesuV2.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IEvnetRepository _eventRepository;
        private readonly UserManager<AppUser> _userManager;

        public CalendarController(AppDbContext context, IUserRepository homeRepository, INotificationRepository notificationRepository, IEvnetRepository eventRepository, UserManager<AppUser> user)
        {
            _context = context;
            _userRepository = homeRepository;
           
            _eventRepository = eventRepository;
            _userManager = user;
        }

        public async Task<IActionResult> Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var currentDate = DateTime.Now;
                var id = _userManager.GetUserId(User);
                AppUser users = await _userRepository.GetUserByIdAsync(id);
                var events = await _eventRepository.GetByUserId(id);


                CalendarViewModel result = new CalendarViewModel { CurrentDate = currentDate, AppUser = users, Events = events.ToList()};

                var forCalendar = events.Select(e => new
                {
                    title = e.Title,
                    start = e.Start,
                    end = e.End,
                    id = e.EventID
                }).ToList();
                ViewBag.Events = JsonSerializer.Serialize(forCalendar);

                return View(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalendarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                    return View(model.Event);
            }
            var id = _userManager.GetUserId(User);
            if (model.StartTime == null)
            {
               
                model.Event.UserID = id;
                _eventRepository.Add(model.Event);
                return RedirectToAction("Index");
            }
            else
            {
                model.Event.Start = model.Event.Start + 'T' + model.StartTime;
                model.Event.End = model.Event.End + 'T' + model.EndTime;
                model.Event.UserID = id;
                _eventRepository.Add(model.Event);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(CalendarViewModel model)
        {
            Event toDelete = await _eventRepository.GetEventByIdAsync(model.Event.EventID);

            _eventRepository.Delete(toDelete);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CalendarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model );
            }
            else
            {
                var id = _userManager.GetUserId(User);
                if (model.StartTime == null)
                {
                   
                    model.Event.UserID = id;
                    _eventRepository.Update(model.Event);
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Event.Start = model.Event.Start + 'T' + model.StartTime;
                    model.Event.End = model.Event.End + 'T' + model.EndTime;
                    model.Event.UserID = id;
                    _eventRepository.Update(model.Event);
                    return RedirectToAction("Index");
                }

            }
        }


    }
}
