using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Migrations;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FabrykaBiznesuV2.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAgendaTaskRepository _agendaTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public TeamController(AppDbContext context, IProjectRepository projectRepository, IAgendaTaskRepository agendaTaskRepository, ITeamRepository teamRepository, UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _context = context;
            _agendaTaskRepository = agendaTaskRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            TeamViewModel model = new TeamViewModel();
            var user = await _userManager.GetUserAsync(User);
            var TeamId = user?.TeamID;
            model.user = user;


            model.team = await _teamRepository.getTeamByID(TeamId.Value);

            var teamMem = await _userRepository.GetByTeamId(TeamId.Value);
            
            model.teamMembers = teamMem.ToList();


            var tasks = _context.AgendaTask
            .Join(_context.Projects,
                task => task.ProjectID,
                project => project.id,
                (task, project) => new AgendaTaskViewModel
                {
                    Id = task.Id,
                    TaskName = task.TaskName,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    UserId = task.UserId,
                    Status = task.Status,
                    SubTasks = task.SubTasks,
                    ProjectName = project.projectName,
            
                })
            .ToList();



            // IEnumerable<AgendaTaskViewModel> tasks = await _agendaTaskRepository.GetByTeamId(TeamId.Value);
            model.teamTasks = tasks;

            IEnumerable<Project> projects = await _projectRepository.GetAll();
            model.projects = projects.ToList();



           var alltasks = _context.AppUsers.Where(user => user.TeamID == TeamId).Select(user => new {user.Id,user.Name, user.LastName}).ToList();

           string tasksJson = JsonConvert.SerializeObject(alltasks.ToList());
      
           ViewBag.TasksJson = tasksJson;


            return View(model);
        }


        public async Task<IActionResult> AssignUser([FromForm] AgendaTaskViewModel item)
        {

            var taksToEdit = _agendaTaskRepository.GetTaskById(item.Id);
            taksToEdit.UserId = item.UserId;
          //  taksToEdit.Status = "inProgress";

            _agendaTaskRepository.Update(taksToEdit);

            return Json(new { success = true, data = item });
        }

    }

}