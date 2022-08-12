using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;
using TechSupportHelpSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OhipBillingController : ControllerBase
    {
        IOHIPClinicService OHIPClinicService;

        public OhipBillingController()
        {
            OHIPClinicService = new OHIPClinicService();
        }

        // GET: <OhipBillingController>
        [HttpGet("{id_Client}")]
        public List<OHIPClinicGroupNumber> Get(int id_Client)
        {
            return OHIPClinicService.GetClinicsOptions(id_Client);
        }

        // GET <OhipBillingController>/5
        [HttpGet("{id_Client}/{id_Clinic}")]
        public List<OHIPClinicGroupNumber> Get(int id_Client, int id_Clinic)
        {
            return OHIPClinicService.GetClinicOptions(id_Client, id_Clinic);
        }

        // POST <OhipBillingController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post([FromBody] OhipClinicNumberDto clinicOptions, int id_Client)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return OHIPClinicService.AddClinicOptions(id_Client, clinicOptions, currentUserClaims);
        }

        // PUT <OhipBillingController>/5
        [HttpPut("{id_Client}")]
        public HttpResponseMessage Put([FromBody] OhipClinicNumberDto clinicOptions, int id_Client)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return OHIPClinicService.EditClinicOptions(id_Client, clinicOptions, currentUserClaims);
        }

        // DELETE <OhipBillingController>/5
        [HttpDelete("{id_Client}/{id_Clinic}/{groupNumber}")]
        public HttpResponseMessage Delete(int id_Client, int id_Clinic, string groupNumber)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return OHIPClinicService.DeleteClinicOptions(id_Client, id_Clinic, groupNumber, currentUserClaims);
        }
    }
}
