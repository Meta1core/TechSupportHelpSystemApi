using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class ClientService : IClientService
    {
        public Client GetClient(int id_Client)
        {
            Client client;
            using (ApplicationContext db = new ApplicationContext(GetMasterOptions()))
            {
                client = db.Client.Where(c => c.ID_Client == id_Client).FirstOrDefault();
            }
            return client;
        }
        public DbContextOptions GetClientOptions(Client client)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseMySql(client.Connection, new MySqlServerVersion(new Version(5, 7)));
            var options = optionsBuilder.Options;
            return options;
        }
        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using (ApplicationContext db = new ApplicationContext(GetMasterOptions()))
            {
                clients = db.Client.ToList();
            }
            return clients;
        }

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
