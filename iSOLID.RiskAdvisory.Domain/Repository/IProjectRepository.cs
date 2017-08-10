using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project this[string name] { get; }
        IEnumerable<Project> GetProjectsForUser(string userName);
    }
}
