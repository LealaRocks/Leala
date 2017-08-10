using iSOLID.RiskAdvisory.Domain.Model;
using iSOLID.RiskAdvisory.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IScriptRepository scripts;

        public ScriptService(IScriptRepository scripts)
        {
            this.scripts = scripts;
        }

        public IEnumerable<Script> GetAllScripts()
        {
            return this.scripts.All();
        }

        public bool TryCreateScript(Script script, out List<ValidationResult> validationResults)
        {
            script.Id = Guid.NewGuid();

            validationResults = new List<ValidationResult>();

            var existing = this.scripts[script.Name];

            if(existing != null)
            {
                validationResults.Add(new ValidationResult("A Script with this name already exists"));
            }

            Validator.TryValidateObject(script, new ValidationContext(script), validationResults);

            if(validationResults.Count == 0)
            {
                this.scripts.Add(script);
            }

            return validationResults.Count == 0;
        }
        public bool TryUpdateScript(Script script, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();

            var existing = this.scripts[script.Name];

            if(existing != null && existing.Id != script.Id)
            {
                validationResults.Add(new ValidationResult("A Script with this name already exists"));
            }

            Validator.TryValidateObject(script, new ValidationContext(script), validationResults);

            if (validationResults.Count == 0)
            {
                this.scripts.Update(script);
            }

            return validationResults.Count == 0;
        }

        public void DeleteScript(Script script)
        {
            this.scripts.Delete(script);
        }
    }
}
