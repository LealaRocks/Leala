using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iSOLID.RiskAdvisory.Domain.Model;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public interface IScriptService
    {
        void DeleteScript(Script script);
        IEnumerable<Script> GetAllScripts();
        bool TryCreateScript(Script script, out List<ValidationResult> validationResults);
        bool TryUpdateScript(Script script, out List<ValidationResult> validationResults);
        Script GetScript(Guid id);
    }
}