using BlazorWorld.Services.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;
        private readonly IUserService _userService;

        public UserController(
            ILogger<NodeController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUserName")]
        public async Task<IActionResult> GetUserNameAsync(string appUserId)
        {
            var userName = await _userService.GetUserNameAsync(appUserId);
            return Ok(userName);
        }

        [HttpGet("GetUserId")]
        public async Task<IActionResult> GetUserIdAsync(string username)
        {
            var userId = await _userService.GetUserIdAsync(username);
            return Ok(userId);
        }

        [HttpGet("GetAvatarHash")]
        public async Task<IActionResult> GetAvatarHashAsync(string appUserId)
        {
            var avatarHash = await _userService.GetAvatarHashAsync(appUserId);
            return Ok(avatarHash);
        }
    }
}