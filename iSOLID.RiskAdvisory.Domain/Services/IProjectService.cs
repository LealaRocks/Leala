using System.Collections.Generic;
using System.Security.Principal;
using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public interface IProjectService
    {
        void DeleteProject(Guid projectId);
        Project GetProject(Guid id, UserBase user);
        IEnumerable<Project> GetProjectsForUser(UserBase user);
        bool TryUpdateProject(Project value, UserBase user, out List<ValidationResult> validationResults);
        bool TryAddProject(Project value, UserBase user, out List<ValidationResult> validationResults);
    }
}