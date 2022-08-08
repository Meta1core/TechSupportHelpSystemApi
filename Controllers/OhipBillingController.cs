using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;
using TechSupportHelpSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OhipBillingController : ControllerBase
    {
        IOHIPClinicService OHIPClinicService = new OHIPClinicService();
        // GET: api/<OhipBillingController>
        [HttpGet("{id_Client}")]
        public List<OHIPClinicGroupNumber> Get(int id_Client)
        {
            return OHIPClinicService.GetClinicsOptions(id_Client);
        }

        // GET api/<OhipBillingController>/5
        [HttpGet("{id_Client}/{id_Clinic}")]
        public List<OHIPClinicGroupNumber> Get(int id_Client, int id_Clinic)
        {
            return OHIPClinicService.GetClinicOptions(id_Client, id_Clinic);
        }

        // POST api/<OhipBillingController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post([FromBody] OHIPClinicGroupNumber clinicOptions, int id_Client)
        {
            return OHIPClinicService.AddClinicOptions(id_Client, clinicOptions);
        }

        // PUT api/<OhipBillingController>/5
        [HttpPut("{id_Client}")]
        public HttpResponseMessage Put([FromBody] OHIPEditClinicNumberDto clinicOptions, int id_Client)
        {
            return OHIPClinicService.EditClinicOptions(id_Client, clinicOptions);
        }

        // DELETE api/<OhipBillingController>/5
        [HttpDelete("{id_Client}/{id_Clinic}/{groupNumber}")]
        public HttpResponseMessage Delete(int id_Client, int id_Clinic, string groupNumber)
        {
            return OHIPClinicService.DeleteClinicOptions(id_Client, id_Clinic, groupNumber);
        }
    }
}
