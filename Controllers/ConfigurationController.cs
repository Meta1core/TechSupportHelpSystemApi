using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigurationController : ControllerBase
    {
        ConfigurationService ConfigurationService;

        public ConfigurationController()
        {
            ConfigurationService = new ConfigurationService();
        }

        // GET: api/<ConfigurationController>
        [HttpGet("{id_Client}")]
        public List<Configuration> Get(int id_Client)
        {
            return ConfigurationService.GetClientConfiguration(id_Client);
        }
        // POST api/<ConfigurationController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post(int id_Client, [FromBody] Configuration configuration)
        {
            return ConfigurationService.EditConfigurationParam(id_Client, configuration);
        }
    }
}
