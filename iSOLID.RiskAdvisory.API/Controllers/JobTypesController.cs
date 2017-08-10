using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iSOLID.RiskAdvisory.Domain.Services;
using iSOLID.RiskAdvisory.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace iSOLID.RiskAdvisory.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class JobTypesController : Controller
    {
        private readonly IJobTypeService jobTypeService;

        public JobTypesController(IJobTypeService jobTypeService)
        {
            this.jobTypeService = jobTypeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobTypes = this.jobTypeService.GetAllJobTypes();
            return Ok(jobTypes);
        }

        [HttpPost]
        public IActionResult Post([FromBody]JobType jobType)
        {
            var validationResults = new List<ValidationResult>();
            this.jobTypeService.TryCreateJobType(jobType, out validationResults);

            return this.ModelUpdate(jobType, validationResults);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] JobType jobType)
        {
            var validationResults = new List<ValidationResult>();
            this.jobTypeService.TryUpdateJobType(jobType, out validationResults);

            return this.ModelUpdate(jobType, validationResults);
        }
    }
}