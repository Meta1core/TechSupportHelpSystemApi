using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        IRoomService roomService = new RoomService();
        // GET: RoomsController
        [HttpGet]
        public async Task<string> Index()
        {
            string roomName = "US1";
            return await Task<string>.Factory.StartNew(() =>
            {
                return roomName;
            });
        }
    }
}
