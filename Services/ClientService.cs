using System.Collections.Generic;
using System.Linq;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class ClientService : IClientService
    {
        public Client GetClient(int id_client)
        {
            Client client;
            using (ApplicationContext db = new ApplicationContext())
            {
                client = db.Client.Where(c => c.ID_Client == id_client).FirstOrDefault();
            }
            return client;
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using (ApplicationContext db = new ApplicationContext())
            {
                clients = db.Client.ToList();
            }
            return clients;
        }
    }
}
