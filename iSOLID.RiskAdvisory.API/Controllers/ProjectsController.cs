using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iSOLID.RiskAdvisory.Domain.Model;
using iSOLID.RiskAdvisory.Domain.Services;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            var user = this.userService.GetByAPIKey(this.HttpContext.Request.Headers["api-key"]);

            return this.projectService.GetProjectsForUser(user);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(Guid id)
        {
            var user = this.userService.GetByAPIKey(this.HttpContext.Request.Headers["api-key"]);
            return this.projectService.GetProject(id, user);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Project value)
        {
            var user = this.userService.GetByAPIKey(this.HttpContext.Request.Headers["api-key"]);

            var validationResults = new List<ValidationResult>();
            this.projectService.TryAddProject(value, user, out validationResults);

            return this.ModelUpdate(value, validationResults);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Project value)
        {
            var user = this.userService.GetByAPIKey(this.HttpContext.Request.Headers["api-key"]);
            var validationResults = new List<ValidationResult>();
            this.projectService.TryUpdateProject(value, user, out validationResults);

            return this.ModelUpdate(value, validationResults);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
