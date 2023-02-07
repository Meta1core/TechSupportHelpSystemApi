using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Repositories;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        IRoomService RoomService;

        public RoomsController(MasterContext masterContext)
        {
            RoomService = new RoomService(masterContext);
        }

        // GET: RoomsController
        [HttpGet("{id_Client}")]
        public List<Room> Get(int id_Client)
        {
            return RoomService.GetRooms(id_Client);
        }

        // GET: RoomsController/id_Client/id_Room
        [HttpGet("{id_Client}/{id_Room}")]
        public Room Get(int id_Client, int id_Room)
        {
            return RoomService.GetRoom(id_Client, id_Room);
        }

        // GET: RoomsController/id_Client/id_Room
        [HttpDelete("{id_Client}/{id_Room}/{id_Modality}")]
        public HttpResponseMessage Delete(int id_Client, int id_Room, int id_Modality)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return RoomService.EditRoomProcedures(id_Client, id_Room, id_Modality, currentUserClaims);
        }

        // POST <RoomsController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post(int id_Client, [FromBody] Room room)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return RoomService.CreateRoom(id_Client, room, currentUserClaims);
        }

        // PUT <RoomsController>/5
        [HttpPut("{id_Client}")]
        public HttpResponseMessage Put(int id_Client, [FromBody] Room room)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return RoomService.UpdateRoom(id_Client, room, currentUserClaims);
        }

        // DELETE <RoomsController>/5
        [HttpDelete("{id_Client}/{id_Room}")]
        public HttpResponseMessage Delete(int id_Client, int id_Room)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return RoomService.DeleteRoom(id_Client, id_Room, currentUserClaims);
        }
    }
}
