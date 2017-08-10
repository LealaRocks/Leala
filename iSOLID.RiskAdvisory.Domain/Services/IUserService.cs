using iSOLID.RiskAdvisory.Domain.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public interface IUserService
    {
        void CreateUser(string firstName, string lastName, string email, string password);

        UserBase GetUser(string email);

        UserBase GetUser(ClaimsPrincipal identity);

        bool ValidateCredentials(string email, string password, out IEnumerable<Claim> claims);
        IEnumerable<UserBase> GetAllUsers();
        bool TryCreateUser(UserBase user, string password, out List<ValidationResult> validationResults);
        bool TryUpdateUser(UserBase user, out List<ValidationResult> validationResults);
        void ResetPassword(Guid id, string password);
        UserBase GetByAPIKey(string apiKey);
    }
}