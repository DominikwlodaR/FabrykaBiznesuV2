using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.Repository;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.EJ2.Base;
using System;
using System.Linq;
using System.Text.Json;

namespace FabrykaBiznesuV2.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEvnetRepository _eventRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IAgendaTaskRepository _agendaTaskRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProjectsController(AppDbContext context, IUserRepository homeRepository, INotificationRepository notificationRepository,IBudgetRepository budgetRepository, IEvnetRepository eventRepository, IProjectRepository projectRepository, IAgendaTaskRepository agendaTaskRepository, UserManager<AppUser> user)
        {
            _context = context;
            _userRepository = homeRepository;
            _notificationRepository = notificationRepository;
            _eventRepository = eventRepository;
            _userManager = user;
            _projectRepository = projectRepository;
            _budgetRepository = budgetRepository;
            _agendaTaskRepository = agendaTaskRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> result = await _projectRepository.GetAll();
            return View(result);
        }


        public async Task<IActionResult> Create()
        {
            var id = _userManager.GetUserId(User);
            IEnumerable<AppUser> users = await _userRepository.GetAll();

            ViewBag.Users = JsonSerializer.Serialize(users);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var id = _userManager.GetUserId(User);
            IEnumerable<AppUser> users = await _userRepository.GetAll();

            ViewBag.Users = JsonSerializer.Serialize(users);
            if (!ModelState.IsValid)
            {
                return View(project);
            }
          

            _projectRepository.Add(project);
            return RedirectToAction("Index");

        }

        public ActionResult Dashboard(int id) 
        {
            Project project = _context.Projects.FirstOrDefault(c => c.id == id);    
            return View(project);
        }

       



     



    }


    
}
