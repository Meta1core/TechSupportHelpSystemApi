using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IRoomService
    {
        List<Room> GetRooms(int id_Сlient);
        Room GetRoom(int id_Client, int id_Room);
        HttpResponseMessage UpdateRoom(int id_Client, Room room, string username);
        HttpResponseMessage DeleteRoom(int id_Client, int id_Room, string username);
        HttpResponseMessage CreateRoom(int id_Client, Room room, string username);
        HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality, string username);
    }
}
