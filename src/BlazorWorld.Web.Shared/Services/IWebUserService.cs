using System.Threading.Tasks;

namespace BlazorWorld.Web.Common.Services
{
    public interface IWebUserService
    {
        Task<string> GetUserNameAsync(string appUserId);
        Task<string> GetUserIdAsync(string username);
        Task<string> GetAvatarHashAsync(string appUserId);
    }
}
