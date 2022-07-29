using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class RoomService : IRoomService
    {
        IClientService ClientService = new ClientService();
        public Room CreateRoom()
        {
            throw new NotImplementedException();
        }

        public Room DeleteRoom()
        {
            throw new NotImplementedException();
        }

        public Room GetRoom()
        {
            throw new NotImplementedException();
        }

        public List<Room> GetRooms(int id_Client)
        {
            List<Room> rooms;
            Client client =  ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                rooms = db.Schdlr_Resource.ToList();
            }
            return rooms;
        }

        public Room UpdateRoom()
        {
            throw new NotImplementedException();
        }
    }
}
