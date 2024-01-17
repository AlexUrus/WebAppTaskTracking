using Microsoft.AspNetCore.Mvc;
using WebAppSibers.Service.Interfaces;

namespace WebAppSibers.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;

        public TaskController(ITaskService taskService, IProjectService projectService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(string statusFilter, string sortOrder)
        {
            var tasksResponce = await _taskService.GetTasksAsync();
            var tasks = tasksResponce.Data;


            if (!string.IsNullOrEmpty(statusFilter))
            {
                tasks = tasks.Where(t => t.Status.ToString() == statusFilter);
            }

            tasks = sortOrder switch
            {
                "TaskName" => tasks.OrderBy(t => t.TaskName),
                "Author" => tasks.OrderBy(t => t.Author.FullName),
                "Assignee" => tasks.OrderBy(t => t.Assignee.FullName),
                "Priority" => tasks.OrderBy(t => t.Priority),
                _ => tasks.OrderBy(t => t.TaskName),
            };
            return View(tasks);
        }

        // GET: Task/Create
        public async Task<IActionResult> Create()
        {
            var employeesResponce = await _employeeService.GetEmployeesAsync();

            ViewBag.Authors = employeesResponce.Data;
            ViewBag.Assignees = employeesResponce.Data;
            ViewBag.Statuses = new List<string> { "ToDo", "InProgress", "Done" };
            var projectsResponce = await _projectService.GetProjectsAsync();
            ViewBag.Projects = projectsResponce.Data;

            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain.Models.Task task)
        {
            await _taskService.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskService.GetTaskByIdAsync((int)id);
            var employeesResponce = await _employeeService.GetEmployeesAsync();
            var projectsResponce = await _projectService.GetProjectsAsync();
            ViewBag.Projects = projectsResponce.Data;
            ViewBag.Assignees = employeesResponce.Data;
            ViewBag.Authors = employeesResponce.Data;

            if (task.Data == null)
            {
                return NotFound();
            }

            return View(task.Data);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Domain.Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            try
            {
                await _taskService.UpdateTaskAsync(task);
            }
            catch (Exception)
            {
                if (await TaskExists(task.Id))
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _taskService.RemoveTaskByIdAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TaskExists(int id)
        {
            var isExistResponce = await _taskService.GetTaskByIdAsync(id);
            return isExistResponce.Data != null;
        }
    }
}
