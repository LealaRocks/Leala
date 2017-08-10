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
    public class ScriptsController : Controller
    {
        private readonly IScriptService scriptService;

        public ScriptsController(IScriptService scriptService)
        {
            this.scriptService = scriptService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.scriptService.GetAllScripts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var script = this.scriptService.GetScript(id);
            if(script == null)
            {
                return NotFound();
            }

            return Ok(script);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Script script)
        {
            this.scriptService.TryCreateScript(script, out List<ValidationResult> validationResults);
            return this.ModelUpdate(script, validationResults);
        }

        [HttpPut("{id")]
        public IActionResult Put([FromBody]Script script)
        {
            this.scriptService.TryUpdateScript(script, out List<ValidationResult> validationResults);
            return this.ModelUpdate(script, validationResults);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var script = this.scriptService.GetScript(id);

            if (script == null)
                return NotFound();

            return Ok();
        }
    }
}