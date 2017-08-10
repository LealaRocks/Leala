using iSOLID.RiskAdvisory.Domain.Model;
using iSOLID.RiskAdvisory.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projects;

        public ProjectService(IProjectRepository projects)
        {
            this.projects = projects;
        }

        public IEnumerable<Project> GetProjectsForUser(UserBase user)
        {
            var projects = this.projects.GetProjectsForUser(user.Email);
            return projects;
        }

        public Project GetProject(Guid id, UserBase user)
        {
            var project = this.projects[id];

            if(project != null && project.ContainsUser(user.Email))
            {
                return project;
            }

            return null;
        }

        public void UpdateProject(Project project)
        {
            this.projects.Update(project);
        }

        public void DeleteProject(Guid projectId)
        {
            var project = this.projects[projectId];
            this.projects.Delete(project);
        }

        public bool TryAddProject(Project project, UserBase user, out List<ValidationResult> validationResults)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            project.Id = Guid.NewGuid();

            validationResults = new List<ValidationResult>();

            var existing = this.projects[project.Name];

            var valid = Validator.TryValidateObject(project, new ValidationContext(project), validationResults);

            if (existing != null && existing.Id != project.Id)
            {
                validationResults.Add(new ValidationResult("A project with this name already exists", new string[] { "Name" }));
                valid = false;
            }

            if(valid)
            {
                project.AddUser(user.Email, Role.Owner);
                this.projects.Add(project);
            }

            return valid;
        }

        public bool TryUpdateProject(Project value, UserBase user, out List<ValidationResult> validationResults)
        {
            throw new NotImplementedException();
        }
        
    }
}
