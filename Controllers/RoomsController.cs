using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<List<Room>> Index()
        {
            string roomName = "US1";
            return await Task<List<Room>>.Factory.StartNew(() =>
            {
                return RoomService.GetRooms();
            });
        }
    }
}
