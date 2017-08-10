using iSOLID.RiskAdvisory.Domain.Model;
using iSOLID.RiskAdvisory.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository users;
        private readonly IProjectRepository projects;

        public UserService(IUserRepository users
            , IProjectRepository projects)
        {
            this.users = users;
            this.projects = projects;
            
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Email = email.ToLower().Trim()
            };

            user.SetPasswordAndSalt(password);

            this.users.Add(user);
        }


        public bool ValidateCredentials(string email, string password, out IEnumerable<Claim> claims)
        {
            
            var user = this.users[email];

            if(user != null && user.ValidatePassword(password))
            {
                user.ApiKey = Guid.NewGuid();

                const string Issuer = "https://iSOLID.co.uk";

                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Name, user.FirstName, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Surname, user.LastName, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String, Issuer),
                    new Claim("api-key", user.ApiKey.ToString(), ClaimValueTypes.String, Issuer)
                };

                users.Update(user);

                return true;
            }
            else if(user != null)
            {
                user.ApiKey = null;
                this.users.Update(user);
            }

            claims = new Claim[0];
            return false;
        }

        public UserBase GetUser(string email)
        {
            var user = this.users[email];
            return user.ToBase();
        }

        public IEnumerable<UserBase> GetAllUsers()
        {
            return this.users.All().Select(x => x.ToBase());
        }

        public bool TryCreateUser(UserBase user, string password, out List<ValidationResult> validationResults)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdateUser(UserBase update, out List<ValidationResult> validationResults)
        {
            update.Email = update.Email.ToLower();

            var user = this.users[update.Id];

            var existingByEmail = this.users[update.Email];
            
            validationResults = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(update, new ValidationContext(update), validationResults);

            if (existingByEmail != null && existingByEmail.Id != update.Id)
            {
                validationResults.Add(new ValidationResult("A user with this email address already exists"));
                valid = false;
            }

            if(valid)
            {
                user.FirstName = update.FirstName;
                user.LastName = update.LastName;
                user.Email = update.Email;
                this.users.Update(user);
            }

            return valid;
        }

        public void ResetPassword(Guid id, string password)
        {
            var user = this.users[id];

            user.SetPasswordAndSalt(password);

            this.users.Update(user);
        }

        public UserBase GetByAPIKey(string apiKey)
        {
            return this.users.GetByApiKey(apiKey);
        }

        public UserBase GetUser(ClaimsPrincipal identity)
        {
            return this.GetUser(identity.FindFirst(ClaimTypes.Email).Value);
        }
    }
}
