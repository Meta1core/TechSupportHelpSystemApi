using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;

namespace TechSupportHelpSystem.Services
{
    public class CashService : ICashService
    {
        IClientService ClientService = new ClientService();

        public HttpResponseMessage AddCashSchedule(int id_Client, CashDto cashSchedule, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    CashSchedule newCash = new CashSchedule() { Name = cashSchedule.CashName };
                    db.Cash_Fee_Schedule.Add(newCash);
                    db.SaveChanges();
                }
                NLogger.Logger.Info("|Client № {0}|User {1} added the new cash fee schedule | ID_CashSchedule - {2}| Title - {3} ", id_Client, currentUserClaims.Value, cashSchedule.CashId, cashSchedule.CashName);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteCashSchedule(int id_Client, int id_CashSchedule, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    CashSchedule cashFromDatabase = db.Cash_Fee_Schedule.Where(c => c.ID_CashSchedule == id_CashSchedule).FirstOrDefault();
                    cashFromDatabase.IsHidden = true;
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} hid the cash fee schedule | ID_CashSchedule - {2}| Title - {3} ", id_Client, currentUserClaims.Value, cashFromDatabase.ID_CashSchedule, cashFromDatabase.Name);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage EditCashSchedule(int id_Client, CashDto cashSchedule, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    CashSchedule cashFromDatabase = db.Cash_Fee_Schedule.Where(r => r.ID_CashSchedule == cashSchedule.CashId).FirstOrDefault();
                    cashFromDatabase.IsHidden = cashSchedule.IsHidden;
                    cashFromDatabase.Name = cashSchedule.CashName;
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} edited the cash fee schedule | ID_CashSchedule - {2}| New title - {3} ", id_Client, currentUserClaims.Value, cashFromDatabase.ID_CashSchedule, cashFromDatabase.Name);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public List<CashSchedule> GetCashSchedules(int id_Client)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.Cash_Fee_Schedule.ToList();
                }
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }
    }
}
