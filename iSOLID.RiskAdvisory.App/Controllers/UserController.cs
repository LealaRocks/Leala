using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iSOLID.RiskAdvisory.App.ViewModels;
using iSOLID.RiskAdvisory.Domain.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace iSOLID.RiskAdvisory.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if(this.ModelState.IsValid)
            {
                IEnumerable<Claim> claims;

                if (this.userService.ValidateCredentials(model.Email, model.Password, out claims))
                {

                    var userIdentity = new ClaimsIdentity(claims, "Passport");

                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                            IsPersistent = model.RememberMe,
                            AllowRefresh = false
                        });


                    Response.Cookies.Append("api-key", userPrincipal.FindFirst("api-key").Value);

                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Projects");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }

            this.ModelState.AddModelError(String.Empty, "Invalid Credentials");
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(this.ModelState.IsValid)
            {
                this.userService.CreateUser(model.FirstName, model.LastName, model.Email, model.Password);
                return RedirectToAction("Login");
            }
            return View(model);
        }
        
    }
}