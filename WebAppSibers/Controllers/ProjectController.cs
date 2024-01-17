using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSibers.DAL.Interfaces;
using WebAppSibers.DAL.Repository;
using WebAppSibers.Domain.Models;
using WebAppSibers.Service.Interfaces;

namespace WebAppSibers.Controllers
{
    [Authorize(Roles = "Leader")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;

        public ProjectController(IProjectService projectService, IEmployeeService employeeService)
        {
            _projectService = projectService;
            _employeeService = employeeService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectsResponce = await _projectService.GetProjectsAsync();
            return View(projectsResponce.Data);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> Create()
        {
            var projectManagersResponce = await _employeeService.GetEmployeesAsync();
            ViewBag.ProjectManagers = projectManagersResponce.Data;
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            var projectManagersResponce = await _employeeService.GetEmployeeByIdAsync(project.ProjectManagerId);
            project.ProjectManager = projectManagersResponce.Data;

            await _projectService.AddProjectAsync(project);

            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectByIdAsync((int)id);
            var projectManagersResponce = await _employeeService.GetEmployeesAsync();
            ViewBag.ProjectManagers = projectManagersResponce.Data;
            if (project.Data == null)
            {
                return NotFound();
            }

            return View(project.Data);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            var projectManagerResponce = await _employeeService.GetEmployeeByIdAsync(project.ProjectManagerId);
            project.ProjectManager = projectManagerResponce.Data;

            try
            {
                await _projectService.UpdateProjectAsync(project);
            }
            catch (Exception)
            {
                if (await ProjectExists(project.Id))
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

            await _projectService.RemoveProjectByIdAsync((int) id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProjectExists(int id)
        {
            var isExistResponce = await _projectService.GetProjectByIdAsync(id);
            return isExistResponce.Data != null;
        }
    }
}
 