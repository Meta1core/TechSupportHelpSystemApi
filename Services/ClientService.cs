using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class ClientService : IClientService
    {
        private MasterContext _masterContext;
        public ClientService(MasterContext masterContext)
        {
            this._masterContext = masterContext;
        }

        public ClientResponseDto FindClientByPrefix(string prefix)
        {
            try
            {
                Client client;
                ClientResponseDto clientResponse;
                using (MasterContext db = this._masterContext)
                {
                    client = db.Client.Where(c => c.Prefix.Equals(prefix)).First();
                    clientResponse = new ClientResponseDto() { ID_Client = client.ID_Client, Connection = client.Connection, IsBlocked = client.IsBlocked, Name = client.Name, Prefix = client.Prefix, PortalUrl = client.PortalUrl };
                    clientResponse.LogoUrl = GetClientLogo(client, client.PortalUrl);
                }
                return clientResponse;
            }
            catch (NullReferenceException e)
            {
                return new ClientResponseDto();
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        private string GetClientLogo(Client client, string clientPortalUrl)
        {
            string clientLogoUrl = "/content/images/logos/";
            if (client.ID_Client < 9) { clientLogoUrl += "000" + client.ID_Client; } else if (client.ID_Client < 100) { clientLogoUrl += "00" + client.ID_Client; } else if (client.ID_Client < 1000) { clientLogoUrl += "0" + client.ID_Client; } else if (client.ID_Client > 999) { clientLogoUrl = client.ID_Client.ToString(); };

            string fullImageUrl = clientPortalUrl + clientLogoUrl + ".png";
            string defaultImageUrl = clientPortalUrl + clientLogoUrl;

            string[] imageTypes = { ".png", ".jpg", ".jpeg" };
            Stream stream = null;
            using (WebClient webClient = new WebClient())
            {
                foreach (string imageType in imageTypes)
                {
                    try
                    {
                        fullImageUrl = defaultImageUrl + imageType;
                        stream = webClient.OpenRead(fullImageUrl);
                        Bitmap bitmap = new Bitmap(stream);
                        return fullImageUrl;
                    }
                    catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                            continue;
                        }
                        else throw new WebException(e.Message);
                    }
                }
            }
            return null;
        }

        public Client GetClient(int id_Client)
        {
            try
            {
                Client client;
                using (MasterContext db = this._masterContext)
                {
                    client = db.Client.Where(c => c.ID_Client == id_Client).FirstOrDefault();
                }
                return client;
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        public DbContextOptions GetClientOptions(Client client)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var connectionString = new MySqlConnectionStringBuilder(client.Connection);
            connectionString.UserID = "d.stryzhak";
            connectionString.Password = "7m5%Yb$s";
            optionsBuilder.UseMySql(connectionString.ConnectionString, new MySqlServerVersion(new Version(5, 7)));
            var options = optionsBuilder.Options;
            return options;
        }

        public List<Client> GetClients()
        {
            try
            {
                List<Client> clients = new List<Client>();
                using (MasterContext db = this._masterContext)
                {
                    clients = db.Client.ToList();
                }
                return clients;
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        [Obsolete("GetMasterOptions is deprecated, now app is using DbContextPool for MasterContext")]
        private DbContextOptions GetMasterOptions()
        {

            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("MasterDatabase"), new MySqlServerVersion(new Version(5, 7)));
            return optionsBuilder.Options;
        }
    }
}
