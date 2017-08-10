using iSOLID.RiskAdvisory.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace iSOLID.RiskAdvisory.API
{
    public class ModelUpdateResult : ActionResult
    {

        private readonly AggregateRoot aggregate;
        private readonly IEnumerable<ValidationResult> validationResults;

        public ModelUpdateResult(AggregateRoot aggregate, IEnumerable<ValidationResult> validationResults)
        {
            this.aggregate = aggregate;
            this.validationResults = validationResults;
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("aggregate-id", this.aggregate.Id.ToString());
            context.HttpContext.Response.Headers.Add("aggregate-version", this.aggregate.Version.ToString());

            if (validationResults != null & validationResults.Any())
            {
                var result = new JsonResult(new
                {
                    ErrorMessages = this.validationResults.Select(x => x.ErrorMessage).ToArray()
                })
                {
                    StatusCode = 422
                };

                result.ExecuteResult(context);
            }
            else
            {
                new EmptyResult().ExecuteResult(context);
            }
        }


    }
}
