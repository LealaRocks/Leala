using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iSOLID.RiskAdvisory.Domain.Services;
using System.Security.Claims;

namespace iSOLID.RiskAdvisory.App.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {

        private readonly IProjectService projectService;
        private readonly IUserService userService;

        public ProjectsController(IProjectService projectService
            , IUserService userService)
        {
            this.projectService = projectService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Details(Guid id)
        {
            var user = this.userService.GetUser(this.User);
            var project = this.projectService.GetProject(id, user);

            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = project.Id;
            ViewBag.ProjectName = project.Name;

            return View();
        }

        public IActionResult Configuration(Guid id)
        {
            var user = this.userService.GetUser(this.User);
            var project = this.projectService.GetProject(id, user);

            if(project == null)
            {
                return NotFound();
            }

            if(project.CanConfiguredBy(user))
            {
                ViewBag.ProjectId = project.Id;
                ViewBag.ProjectName = project.Name;

                return View();
            }

            return Forbid();

        }
    }
}