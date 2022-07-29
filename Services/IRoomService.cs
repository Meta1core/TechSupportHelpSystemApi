using System.Collections.Generic;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IRoomService
    {
        List<Room> GetRooms(int id_Сlient);
        Room GetRoom();
        Room UpdateRoom();
        Room DeleteRoom();
        Room CreateRoom();
    }
}
