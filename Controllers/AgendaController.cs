using FabrykaBiznesuV2.Data;
using FabrykaBiznesuV2.Interfaces;
using FabrykaBiznesuV2.Migrations;
using FabrykaBiznesuV2.Models;
using FabrykaBiznesuV2.Repository;
using FabrykaBiznesuV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;



namespace FabrykaBiznesuV2.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEvnetRepository _eventRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IAgendaTaskRepository _agendaTaskRepository;
        private readonly IAgendaCommentsRepository _agendaCommentsRepository;
        private readonly UserManager<AppUser> _userManager;

        public AgendaController(AppDbContext context, IUserRepository homeRepository, IAgendaCommentsRepository agendaCommentsRepository, INotificationRepository notificationRepository, IBudgetRepository budgetRepository, IEvnetRepository eventRepository, IProjectRepository projectRepository, IAgendaTaskRepository agendaTaskRepository, UserManager<AppUser> user)
        {
            _context = context;
            _userRepository = homeRepository;
            _notificationRepository = notificationRepository;
            _eventRepository = eventRepository;
            _userManager = user;
            _projectRepository = projectRepository;
            _budgetRepository = budgetRepository;
            _agendaTaskRepository = agendaTaskRepository;
            _agendaCommentsRepository = agendaCommentsRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            Project project = _context.Projects.FirstOrDefault(c => c.id == id);
            AgendaTaskModelView model = new AgendaTaskModelView();

            IEnumerable<AgendaTask> c = await _agendaTaskRepository.GetByProjectId(id);
            IEnumerable<AgendaComments> comms = await _agendaCommentsRepository.GetByProjectId(id);
            List<AgendaTask> coll = new List<AgendaTask>();
            List<AgendaComments> comments = new List<AgendaComments>();
            foreach (AgendaTask item in c)
            {
              
                coll.Add(item);
            }
            foreach(AgendaComments comment in comms)
            {
                comments.Add(comment);
            }
            model.project = project;
            model.agendaTaskCollection = coll.OrderBy(o => o.TaskId).ToList();
            model.comments = comments;

            return View(model);
        }

 
       


        [HttpPost]
        public async Task<IActionResult> AddTaskBelow([FromForm] AgendaTask itemToAdd)
        {
            IEnumerable<AgendaTask> c = await _agendaTaskRepository.GetByProjectId(itemToAdd.ProjectID);
            foreach (AgendaTask agendaTask in c)
            {
                if (agendaTask.TaskId >= itemToAdd.TaskId)
                {
                    agendaTask.TaskId++;
                    _agendaTaskRepository.Update(agendaTask);
                }


                if (agendaTask.Predecessor != null)
                {
                    int st = int.Parse(agendaTask.Predecessor.Substring(0, agendaTask.Predecessor.Length - 2));
                    string relation = agendaTask.Predecessor.Substring(agendaTask.Predecessor.Length - 2);
                    if (st >= itemToAdd.TaskId)
                    {
                        st++;
                        agendaTask.Predecessor = st.ToString() + relation;
                        _agendaTaskRepository.Update(agendaTask);
                    }
                }

            }
                DateTime todayDate = DateTime.Today;
            itemToAdd.StartDate = todayDate;
            itemToAdd.EndDate = todayDate;
            itemToAdd.Status = "planned";
            _agendaTaskRepository.Add(itemToAdd);

            return Json(new { success = true, data = itemToAdd });
        }

        public async Task<IActionResult> AddTask([FromForm] AgendaTask item)
        {
            item.isSubtask = false;
           
            if (!item.isSubtask)
            {
                item.IsManual = true;
            }
            else { 
            item.IsManual = false;

            }
            item.Status = "planned";
            _agendaTaskRepository.Add(item);
            return Json(new { success = true, data = item });
        }


        public async Task<IActionResult> EditTask([FromForm] AgendaTask item)
        {
           
            _agendaTaskRepository.Update(item);

            return Json(new { success = true, data = item });
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskToDelete = _agendaTaskRepository.GetTaskById(id);

            IEnumerable<AgendaTask> c = await _agendaTaskRepository.GetByProjectId(taskToDelete.ProjectID);
            List<AgendaTask> tasks = c.ToList();

            foreach (AgendaTask task in tasks)
            {
                if (task.TaskId > taskToDelete.TaskId)
                {
                    if (taskToDelete.SubTasks != null && taskToDelete.SubTasks.Any())
                    {
                        task.TaskId = task.TaskId - taskToDelete.SubTasks.Count() - 1;
                    }
                    else
                    {
                        task.TaskId--;
                    }
                    _agendaTaskRepository.Update(task);
                }
                
                if (task.Predecessor != null)
                {
                    string st = task.Predecessor.Substring(0, task.Predecessor.Length - 2);
                    string relation = task.Predecessor.Substring(task.Predecessor.Length - 2);

                    if (st == taskToDelete.TaskId.ToString())
                    {
                        task.Predecessor = null;
                        _agendaTaskRepository.Update(task); 
                    }
                    int l = int.Parse(st);
                    if(l > taskToDelete.TaskId) {
                        if (taskToDelete.SubTasks != null && taskToDelete.SubTasks.Any())
                        {
                            l = l  - taskToDelete.SubTasks.Count() - 1;
                            task.Predecessor = l.ToString() + relation;
                        }
                        else
                        {
                            l--;
                            task.Predecessor = l.ToString() + relation;
                        }
                        _agendaTaskRepository.Update(task);
                    }
                }
               


            }

            if (taskToDelete.SubTasks != null && taskToDelete.SubTasks.Any()) { 
                foreach (var subtask in taskToDelete.SubTasks.ToList())
                {
                    _agendaTaskRepository.Delete(subtask);
                }

            }


            _agendaTaskRepository.Delete(taskToDelete);
            return Json(new { success = true });
        }


        public async Task<IActionResult> AddSubTask(int id)
        {

            AgendaTask task = _agendaTaskRepository.GetTaskById(id);

            IEnumerable<AgendaTask> c = await _agendaTaskRepository.GetByProjectId(task.ProjectID);



            if (task.SubTasks != null && task.SubTasks.Any()) {

                AgendaTask Child1 = new AgendaTask()
                {
                    TaskId = task.SubTasks.Last().TaskId + 1,
                    TaskName = "Nowe podzadanie",
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ProjectID = task.ProjectID,
                    SubTasks = new List<AgendaTask>(),
                    isSubtask = true,
                    Status = "planned",
                    TeamId = 0


                };
                foreach (var item in c)
                {
                    if (item.TaskId >= Child1?.TaskId)
                    {
                        item.TaskId++;
                    }
                    if (item.Predecessor != null)
                    {
                        int st = int.Parse(item.Predecessor.Substring(0, item.Predecessor.Length - 2));
                        string relation = item.Predecessor.Substring(item.Predecessor.Length - 2);
                        if (st >= item.TaskId)
                        {
                            st++;
                            item.Predecessor = st.ToString() + relation;
                            _agendaTaskRepository.Update(item);
                        }
                    }
                
                }
                task.SubTasks.Add(Child1);
              
            }
            else
            {
                AgendaTask Child1 = new AgendaTask()
                {
                    TaskId = task.TaskId + 1,
                    TaskName = "Nowe zadanie",
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ProjectID = task.ProjectID,
                    SubTasks = new List<AgendaTask>(),
                    isSubtask = true,
                    Status = "planned",
                    TeamId = 0


                };
                foreach (var item in c)
                {
                    if (item.TaskId >= Child1?.TaskId)
                    {
                        item.TaskId++;
                    }
                }
                task.SubTasks = new List<AgendaTask> { Child1};
            }


            _agendaTaskRepository.Update(task);

            return Json(new { success = true, data = task });
        }

        public async Task<IActionResult> AddComm([FromForm] AgendaComments item)
        {
            _agendaCommentsRepository.Add(item);
            return Json(new { success = true, data = item });
        }

        public async Task<IActionResult> AddRelay([FromForm] AgendaComments item)
        {
            AgendaComments parent = _agendaCommentsRepository.GetById(item.ParentId.Value);
         
            parent.Replies = new List<AgendaComments> { item };

            _agendaCommentsRepository.Update(parent);
            return Json(new { success = true, data = item });
        }

        public async Task<IActionResult> DeleteComm(int id)
        {
            AgendaComments commToDelete = _agendaCommentsRepository.GetById(id);

            IEnumerable<AgendaComments> reps = await _agendaCommentsRepository.GetByProjectId(commToDelete.ProjectId);
            foreach (var item in reps)
            {
                if(item.ParentId == commToDelete.Id)
                {
                    _agendaCommentsRepository.Delete(item);
                }
            }



            _agendaCommentsRepository.Delete(commToDelete);

            return Json(new { success = true });
        }

    }
}
