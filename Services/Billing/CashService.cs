using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class CashService : ICashService
    {
        private IClientService ClientService;
        private IRepository<CashSchedule> CashRepository;

        public CashService(MasterContext masterContext)
        {
            ClientService = new ClientService(masterContext);
            CashRepository = new CashRepository();
        }

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
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteCashSchedule(int id_Client, int id_CashSchedule, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                CashSchedule hiddenCashSchedule = CashRepository.Delete(clientOptions, id_CashSchedule);
                NLogger.Logger.Info("|Client № {0}|User {1} hid the cash fee schedule | ID_CashSchedule - {2}| Title - {3} ", id_Client, currentUserClaims.Value, hiddenCashSchedule.ID_CashSchedule, hiddenCashSchedule.Name);
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public HttpResponseMessage EditCashSchedule(int id_Client, CashDto cashSchedule, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                CashSchedule updatedCashSchedule = CashRepository.Update(clientOptions, new CashSchedule() { ID_CashSchedule = (int)cashSchedule.CashId, IsHidden = (bool)cashSchedule.IsHidden, Name = cashSchedule.CashName });
                NLogger.Logger.Info("|Client № {0}|User {1} edited the cash fee schedule | ID_CashSchedule - {2}| New title - {3} ", id_Client, currentUserClaims.Value, updatedCashSchedule.ID_CashSchedule, updatedCashSchedule.Name);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public List<CashSchedule> GetCashSchedules(int id_Client)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                return CashRepository.GetAll(clientOptions);
            }
            catch (Exception e)
            {
                NLogger.Logger.Error("|Client № {0}|Exception: {1}", id_Client, e);
                return null;
            }
        }
    }
}
