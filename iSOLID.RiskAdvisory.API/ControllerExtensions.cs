using iSOLID.RiskAdvisory.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace iSOLID.RiskAdvisory.API
{
    public static class ControllerExtensions
    {
        public static IActionResult ModelUpdate(this Controller controller, AggregateRoot aggregate)
        {
            return ModelUpdate(controller, aggregate, new ValidationResult[0]);
        }

        public static IActionResult ModelUpdate(this Controller controller, AggregateRoot aggregate, IEnumerable<ValidationResult> validationResults)
        {
            return new ModelUpdateResult(aggregate, validationResults);
        }
    }
}
