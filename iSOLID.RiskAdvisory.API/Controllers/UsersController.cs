using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iSOLID.RiskAdvisory.Domain.Services;
using iSOLID.RiskAdvisory.Domain.Model;
using System.ComponentModel.DataAnnotations;
using iSOLID.RiskAdvisory.API.ViewModels;

namespace iSOLID.RiskAdvisory.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = this.userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserBase user, string password, string confirmPassword)
        {
            var validationResults = new List<ValidationResult>();

            if (password != confirmPassword)
            {
                this.userService.TryCreateUser(user, password, out validationResults);
            }
            else
            {
                validationResults.Add(new ValidationResult("Passwords do not match"));
            }

            return this.ModelUpdate(user, validationResults);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserBase user)
        {
            var validationResults = new List<ValidationResult>();
            this.userService.TryUpdateUser(user, out validationResults);

            return this.ModelUpdate(user, validationResults);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, [FromBody] ResetPasswordPatch patch)
        {
            if(ModelState.IsValid)
            {
                this.userService.ResetPassword(id, patch.Password);
                return Ok();
            }
            return new StatusCodeResult(500);
        }
    }
}