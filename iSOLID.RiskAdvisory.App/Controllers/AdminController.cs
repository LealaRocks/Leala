////using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace iSOLID.RiskAdvisory.App.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            if (ViewBag.DefaultView == null)
                ViewBag.DefaultView = Url.Action("Clients");

            return View();
        }

        public IActionResult Clients()
        {
            if(Request.IsAjaxRequest())
            {
                return PartialView();
            }


            ViewBag.DefaultView = Url.Action("Clients");
            return View("Index");
        }

        public IActionResult Projects()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            ViewBag.DefaultView = Url.Action("Projects");
            return View("Index");
        }

        public IActionResult Users()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            ViewBag.DefaultView = Url.Action("Users");
            return View("Index");
        }

        public IActionResult JobTypes()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            ViewBag.DefaultView = Url.Action("JobTypes");
            return View("Index");
        }
    }
}