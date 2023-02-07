using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class ConfigurationService
    {
        IClientService ClientService;

        public ConfigurationService(MasterContext masterContext)
        {
            ClientService = new ClientService(masterContext);
        }

        public List<Configuration> GetClientConfiguration(int id_Client)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.Configuration.ToList();
                }
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        public HttpResponseMessage EditConfigurationParam(int id_Client, Configuration configuration, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    if (db.Configuration.Where(c => c.ParameterName == configuration.ParameterName).FirstOrDefault() is null)
                    {
                        AddNewConfigurationParameter(configuration, db);
                    }
                    else
                    {
                        EditConfigurationParameter(configuration, db);
                    }
                    NLogger.Logger.Info("|Client № {0}|User {1} edited the configuration parameter - {2} | New value {3} ", id_Client, currentUserClaims.Value, configuration.ParameterName, configuration.ParameterValue);
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

        private void EditConfigurationParameter(Configuration configuration, ApplicationContext db)
        {
            Configuration configurationFromDatabase = db.Configuration.Where(c => c.ParameterName == configuration.ParameterName).FirstOrDefault();
            configurationFromDatabase.ParameterValue = configuration.ParameterValue;
            configurationFromDatabase.ID_Clinic = configuration.ID_Clinic;
            db.Entry(configurationFromDatabase).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void AddNewConfigurationParameter(Configuration configuration, ApplicationContext db)
        {
            db.Configuration.Add(configuration);
            db.Entry(configuration).State = EntityState.Added;
            db.SaveChanges();
        }
    }
}
