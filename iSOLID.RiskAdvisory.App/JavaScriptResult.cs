using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSOLID.RiskAdvisory.App
{
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult()
        {
            this.ContentType = "application/javascript";
        }

        public JavaScriptResult(string script)
            : this()
        {
            this.Content = script;
        }

        public JavaScriptResult Append(string script)
        {
            this.Content += Environment.NewLine;
            this.Content += script;

            return this;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
            context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
            context.HttpContext.Response.Headers.Add("Expires", "0");

            return base.ExecuteResultAsync(context);
        }
    }
}
