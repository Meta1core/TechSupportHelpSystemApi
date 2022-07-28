using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        IClientService ClientService = new ClientService();
        // GET: api/<ClientsController>
        [HttpGet]
        public List<Client> Get()
        {
            return ClientService.GetClients();
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public Client Get(int id)
        {
            return ClientService.GetClient(id);
        }

        // POST api/<ClientsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
