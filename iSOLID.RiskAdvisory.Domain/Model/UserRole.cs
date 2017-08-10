using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class UserRole
    {
        public UserRole()
        {
        }

        public UserRole(string userName, Role role)
        {
            this.UserName = userName;
            this.Role = role;
        }

        public string UserName { get; set; }

        public Role Role { get; set; }
    }
}
