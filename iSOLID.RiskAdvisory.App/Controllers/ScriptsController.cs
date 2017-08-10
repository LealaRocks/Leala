using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iSOLID.RiskAdvisory.App.Controllers
{
    [Authorize]
    public class ScriptsController : Controller
    {
        private readonly IOptions<AppSettings> appSettings;

        public ScriptsController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResourceUrls()
        {
            var script = String.Format(@"leala.apiUrls = {{
                clients: '{0}/api/clients',
                projects: '{0}/api/projects',
                jobTypes: '{0}/api/jobtypes',
                users: '{0}/api/users'
            }}", appSettings.Value.ApiURL);

            return new JavaScriptResult(script);
        }


    }
}
