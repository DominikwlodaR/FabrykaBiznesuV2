using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.Repository;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FabrykaBiznesuV2.Controllers
{
    public class ResourcesController: Controller
    {
        private readonly AppDbContext _context;
        private readonly IAgendaTaskRepository _agendaTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;
        public ResourcesController(AppDbContext context, IProjectRepository projectRepository, IAgendaTaskRepository agendaTaskRepository, ITeamRepository teamRepository)
        {
            _context = context;
            _agendaTaskRepository = agendaTaskRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = new ResourcesViewModel();
            Project project = _context.Projects.FirstOrDefault(c => c.id == id);
            IEnumerable<AgendaTask> tasks = await _agendaTaskRepository.GetByProjectId(id);
            IEnumerable<Team> teams = await _teamRepository.GetAll();


            IEnumerable<AgendaTask> alltasks = await _agendaTaskRepository.GetAll();
            string teamsJson = JsonConvert.SerializeObject(teams.ToList());
            string tasksJson = JsonConvert.SerializeObject(alltasks.ToList());
            ViewBag.TeamsJson = teamsJson;
            ViewBag.TasksJson = tasksJson;
            model.project = project;
            model.tasks = tasks.ToList();
            model.teams = teams.ToList();   

            return View(model);
        }


        public async Task<IActionResult> AssignTeam([FromForm] AgendaTask item)
        {

            var taksToEdit = _agendaTaskRepository.GetTaskById(item.Id);
            taksToEdit.TeamId = item.TeamId;
            taksToEdit.Status = "inProgress";

            _agendaTaskRepository.Update(taksToEdit);

            return Json(new { success = true, data = item });
        }


    }


}
