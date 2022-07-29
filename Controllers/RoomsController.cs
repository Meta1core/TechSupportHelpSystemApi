using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        IRoomService RoomService = new RoomService();
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
        [HttpGet("{id_Client}/{id_Room}/{id_Modality}")]
        public HttpResponseMessage Get(int id_Client, int id_Room, int id_Modality)
        {
            return RoomService.EditRoomProcedures(id_Client, id_Room, id_Modality);
        }

        // POST api/<RoomsController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post(int id_Client, [FromBody] Room room)
        {
            return RoomService.CreateRoom(id_Client, room);
        }

        // PUT api/<RoomsController>/5
        [HttpPut("{{id_Client}}")]
        public HttpResponseMessage Put(int id_Client, [FromBody] Room room)
        {
            return RoomService.UpdateRoom(id_Client, room);
        }

        // DELETE api/<RoomsController>/5
        [HttpDelete("{id_Client}/{id_Room}")]
        public HttpResponseMessage Delete(int id_Client, int id_Room)
        {
            return RoomService.DeleteRoom(id_Client, id_Room);
        }
    }
}
