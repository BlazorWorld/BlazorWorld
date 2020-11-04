using BlazorWorld.Services.Content;
using BlazorWorld.Web.Shared.Services;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerUserService : IWebUserService
    {
        private readonly IUserService _userService;

        public ServerUserService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> GetAvatarHashAsync(string appUserId)
        {
            return await _userService.GetAvatarHashAsync(appUserId);
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            return await _userService.GetUserIdAsync(username);
        }

        public async Task<string> GetUserNameAsync(string appUserId)
        {
            return await _userService.GetUserNameAsync(appUserId);
        }
    }
}
