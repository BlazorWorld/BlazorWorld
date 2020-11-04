using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebSecurityService
    {
        Task<bool> AllowedAsync(string loggedInUserId,
            string createdBy,
            string module,
            string type,
            string action);
    }
}
