using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iSOLID.RiskAdvisory.Domain.Services;
using iSOLID.RiskAdvisory.Domain.Model;
using System.Net.Http;

namespace iSOLID.RiskAdvisory.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientService clientService;


        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var clients = this.clientService.GetAllClients().ToArray();
            return this.Ok(clients);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var client = this.clientService.GetClient(id);
            return this.Ok(client);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Client client)
        {
            this.clientService.AddClient(client);
            return this.ModelUpdate(client);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Client client)
        {
            this.clientService.Update(client);
            return this.ModelUpdate(client);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return this.Ok();
        }
    }
}