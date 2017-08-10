using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class Project : AggregateRoot
    {

        public Project()
        {
            this.Users = new List<UserRole>();
            this.Tabs = new List<Tab>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Guid ClientId { get; set; }

        public Guid TypeId { get; set; }

        public List<UserRole> Users { get; set; }

        public List<Tab> Tabs { get; set; }


        public void AddUser(string userName, Role role)
        {
            var existing = this.Users.FirstOrDefault(x => String.Equals(x.UserName, userName, StringComparison.CurrentCultureIgnoreCase));

            if(existing != null)
            {
                existing.Role = role;
            }
            else
            {
                Users.Add(new UserRole(userName, role));
            }
        }

        public void AddUser(User user, Role role)
        {
            this.AddUser(user.Email, role);
        }

        public void AddUser(IIdentity user, Role role)
        {
            this.AddUser(user.Name, role);
        }

        public bool ContainsUser(string name)
        {
            return this.Users
                .Any(x => x.UserName == name);
        }

        public bool CanConfiguredBy(UserBase user)
        {
            var role = this.GetRole(user);

            if(role.HasValue)
            {
                switch (role.Value)
                {
                    case Role.Owner:
                    case Role.ProjectManager:
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }

        public Role? GetRole(UserBase user)
        {
            var userRole = this.Users
                .Where(x => x.UserName == user.Email)
                .Select(x => new Nullable<Role>(x.Role))
                .FirstOrDefault();

            return userRole;
        }
    }
}
