using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User this[string email] { get; }

        IEnumerable<User> GetForProject(Project project);
        IEnumerable<User> All();
        User GetByApiKey(string apiKey);
    }
}
