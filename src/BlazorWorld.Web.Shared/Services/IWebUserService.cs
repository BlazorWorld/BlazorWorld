using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebUserService
    {
        Task<string> GetUserNameAsync(string appUserId);
        Task<string> GetUserIdAsync(string username);
        Task<string> GetAvatarHashAsync(string appUserId);
    }
}
