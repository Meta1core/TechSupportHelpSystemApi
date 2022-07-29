using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IRoomService
    {
        List<Room> GetRooms(int id_Сlient);
        Room GetRoom(int id_Client, int id_Room);
        HttpResponseMessage UpdateRoom(int id_Client, Room room);
        HttpResponseMessage DeleteRoom(int id_Client, int id_Room);
        HttpResponseMessage CreateRoom(int id_Client, Room room);
        HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality);
    }
}
