using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.SmartParking.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public string Login([FromForm] string userName, [FromForm] string password)
        {
            return "{\"State\":\"True\"}";
        }
    }
}
