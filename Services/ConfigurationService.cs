using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class ConfigurationService
    {
        IClientService ClientService = new ClientService();
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
                return null; // Nlog
            }
        }

        public HttpResponseMessage EditConfigurationParam(int id_Client, Configuration configuration)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    if (db.Configuration.Where(c => c.ParameterName == configuration.ParameterName).FirstOrDefault() is null)
                    {
                        db.Configuration.Add(configuration);
                        db.SaveChanges();
                    }
                    else
                    {
                        Configuration configurationFromDatabase = db.Configuration.Where(c => c.ParameterName == configuration.ParameterName).FirstOrDefault();
                        configurationFromDatabase.ParameterValue = configuration.ParameterValue;
                        configurationFromDatabase.ID_Clinic = configuration.ID_Clinic;
                        db.SaveChanges();
                    }
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }
    }
}
