using System.Collections.Generic;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IRoomService
    {
        List<Room> getRooms();
        Room getRoom();
        Room updateRoom();
        Room deleteRoom();
        Room createRoom();
    }
}
