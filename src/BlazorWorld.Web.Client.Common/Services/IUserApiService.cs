using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Common.Services
{
    public interface IUserApiService
    {
        Task<string> GetUserNameAsync(string appUserId);
        Task<string> GetUserIdAsync(string username);
        Task<string> GetAvatarHashAsync(string appUserId);
    }
}
