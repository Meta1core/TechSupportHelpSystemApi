using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [HttpGet("{id}")]
        public List<Room> Get(int id)
        {
            return RoomService.GetRooms(id);
        }
    }
}
