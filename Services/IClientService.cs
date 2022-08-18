using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;

namespace TechSupportHelpSystem.Services
{
    interface IClientService
    {
        List<Client> GetClients();
        Client GetClient(int id_Client);
        ClientResponseDto FindClientByPrefix(string prefix);
        DbContextOptions GetClientOptions(Client client);
    }
}
