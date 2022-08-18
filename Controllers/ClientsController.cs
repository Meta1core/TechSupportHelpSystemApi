using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        IClientService ClientService;

        public ClientsController()
        {
            ClientService = new ClientService();
        }

        // GET: <ClientsController>
        [HttpGet]
        public List<Client> Get()
        {
            return ClientService.GetClients();
        }

        // GET: <ClientsController>
        [HttpGet("{prefix}")]
        public ClientResponseDto Get(string prefix)
        {
            return ClientService.FindClientByPrefix(prefix);
        }
    }
}
