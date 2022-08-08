using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public List<Client> Get()
        {
            return ClientService.GetClients();
        }

        // GET: api/<ClientsController>
        [HttpGet("{prefix}")]
        [Authorize]
        public Client Get(string prefix)
        {
            return ClientService.FindClientByPrefix(prefix);
        }
    }
}
