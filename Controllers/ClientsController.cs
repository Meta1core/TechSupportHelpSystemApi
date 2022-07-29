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

    }
}
