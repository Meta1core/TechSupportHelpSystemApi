using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;

namespace TechSupportHelpSystem.Services
{
    interface ICashService
    {
        public List<CashSchedule> GetCashSchedules(int id_Client);
        public HttpResponseMessage AddCashSchedule(int id_Client, CashDto cashSchedule, Claim currentUserClaims);
        public HttpResponseMessage EditCashSchedule(int id_Client, CashDto cashSchedule, Claim currentUserClaims);
        public HttpResponseMessage DeleteCashSchedule(int id_Client, int id_CashSchedule, Claim currentUserClaims);
    }
}
