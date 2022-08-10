using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IRoomService
    {
        List<Room> GetRooms(int id_Сlient);
        Room GetRoom(int id_Client, int id_Room);
        HttpResponseMessage UpdateRoom(int id_Client, Room room, Claim currentUserClaims);
        HttpResponseMessage DeleteRoom(int id_Client, int id_Room, Claim currentUserClaims);
        HttpResponseMessage CreateRoom(int id_Client, Room room, Claim currentUserClaims);
        HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality, Claim currentUserClaims);
    }
}
