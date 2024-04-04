using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.Repository;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FabrykaBiznesuV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEvnetRepository _eventRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITeamRepository _teamRepository;


        public HomeController(AppDbContext context, IUserRepository homeRepository, INotificationRepository notificationRepository, IEvnetRepository eventRepository, UserManager<AppUser> user, ITeamRepository teamRepository)
        {
            _context = context;
            _userRepository = homeRepository;
            _notificationRepository = notificationRepository;
            _eventRepository = eventRepository;
            _teamRepository = teamRepository;
            _userManager = user;
        }

       
        public async Task <IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                
                var id = _userManager.GetUserId(User);
                AppUser users = await _userRepository.GetUserByIdAsync(id);
                var nots = await _notificationRepository.GetByUserId();
                var events = await _eventRepository.GetByUserId(id);

                homeModel result = new homeModel { AppUser = users, Events = events.ToList(), Notifications = nots.ToList() };

                return View(result);
            }
        }
        [Authorize]
        public IActionResult Create()
        {
            CreateViewModel model = new CreateViewModel();
            var error = TempData["Error"] as string;
            if (!string.IsNullOrEmpty(error))
            {
                // Tutaj możesz obsłużyć informacje o błędzie, np. przekazać do widoku
                ViewBag.ErrorMessage = error;
            }

            return View(model);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateViewModel ev)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(ev);
        //    }
        //    _eventRepository.Add(ev.Event);
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] AppUser user)
        {
            var adminUser = await _userManager.FindByEmailAsync(user.Email);
            if (adminUser == null)
            {

                var newUser = new AppUser()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = true,
                    TeamID = user.TeamID

                };
                var result = await _userManager.CreateAsync(newUser, "Haslo@1234?");
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                if (!result.Succeeded)
                {
                    
                    await _context.SaveChangesAsync();
                    return Json(new { success = false, errors = result.Errors });
                }
                else
                {
                    return Json(new { success = true, data = user });
                }

            }
            else
            {
                return Json(new { success = false, data = user });
            } 
        }


        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromForm] Team team)
        {
            
            _teamRepository.Add(team);
            return Json(new { success = false, data = team });
        }



    }
}