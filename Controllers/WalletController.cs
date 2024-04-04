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
    public class WalletController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEvnetRepository _eventRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IAgendaTaskRepository _agendaTaskRepository;
        private readonly UserManager<AppUser> _userManager;

        public WalletController(AppDbContext context, IUserRepository homeRepository, INotificationRepository notificationRepository, IBudgetRepository budgetRepository, IEvnetRepository eventRepository, IProjectRepository projectRepository, IAgendaTaskRepository agendaTaskRepository, UserManager<AppUser> user)
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

        [HttpPost]
        public async Task<IActionResult> Index(BudgetModel budgetModel)
        {

            if (!ModelState.IsValid)
            {

                return View(budgetModel.BAdd);
            }


            _budgetRepository.Add(budgetModel.BAdd);
            return RedirectToAction("Index");

        }


        public async Task<ActionResult> Index(int id)
        {
            Project project = _context.Projects.FirstOrDefault(c => c.id == id);
            var Userid = _userManager.GetUserId(User);
            AppUser user = await _userRepository.GetUserByIdAsync(Userid);

            IEnumerable<Budget> budgets = await _budgetRepository.GetByProjectId(id);
            List<Budget> budgetsList = budgets.ToList();

            double spend = 0;
            double noAccept = 0;
            int acceptedCounter = 0;
            int waintingCounter = 0;
            int rejectedCounter = 0;
            foreach (var b in budgetsList)
            {
                if (b.status == "Zaakceptowane")
                {
                    spend += b.sum;
                    acceptedCounter++;
                }
                if (b.status == "Do akceptacji")
                {
                    noAccept += b.sum;
                    waintingCounter++;
                }
                if (b.status == "Odrzucone")
                {
                    rejectedCounter++;
                }

            }
            var budgetData = new { data = new[] { project.ProjectBudget, spend, noAccept } };
            ViewBag.BudgetData = JsonSerializer.Serialize(budgetData);


            BudgetModel result = new BudgetModel { Project = project, appUser = user, Budgets = budgetsList, MoneySpend = spend, NoAccept = noAccept, AcceptedCounter = acceptedCounter, WaintingCounter = waintingCounter, RejectedCounter = rejectedCounter };
            return View(result);
        }


        public IActionResult Accept(int id)
        {
            var item = _budgetRepository.GetSumById(id);
            item.status = "Zaakceptowane";
            _budgetRepository.Update(item);
            return RedirectToAction("index", new { id = item.projectId });
        }

        public IActionResult Reject(int id)
        {
            var item = _budgetRepository.GetSumById(id);
            item.status = "Odrzucone";
            _budgetRepository.Update(item);
            return RedirectToAction("index", new { id = item.projectId });
        }

        public IActionResult Delete(int id)
        {
            var item = _budgetRepository.GetSumById(id);
            _budgetRepository.Delete(item);
            return RedirectToAction("index", new { id = item.projectId });
        }

    }
}
