using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface ICashService
    {
        public List<CashSchedule> GetCashSchedules(int id_Client);
        public HttpResponseMessage AddCashSchedule(int id_Client, CashSchedule cashSchedule);
        public HttpResponseMessage EditCashSchedule(int id_Client, CashSchedule cashSchedule);
        public HttpResponseMessage DeleteCashSchedule(int id_Client, int id_CashSchedule);
    }
}
