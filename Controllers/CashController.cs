using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CashController : ControllerBase
    {
        ICashService CashService = new CashService();
        // GET: api/<CashController>
        [HttpGet("{id_Client}")]
        public List<CashSchedule> Get(int id_Client)
        {
            return CashService.GetCashSchedules(id_Client);
        }

        // POST api/<CashController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post(int id_Client, [FromBody] CashSchedule cashSchedule)
        {
            return CashService.AddCashSchedule(id_Client, cashSchedule);
        }

        // PUT api/<CashController>/5
        [HttpPut("{id_Client}")]
        public HttpResponseMessage Put(int id_Client, [FromBody] CashSchedule cashSchedule)
        {
            return CashService.EditCashSchedule(id_Client, cashSchedule);
        }

        // DELETE api/<CashController>/5
        [HttpDelete("{id_Client}/{id_CashSchedule}")]
        public HttpResponseMessage Delete(int id_Client, int id_CashSchedule)
        {
            return CashService.DeleteCashSchedule(id_Client, id_CashSchedule);
        }
    }
}
