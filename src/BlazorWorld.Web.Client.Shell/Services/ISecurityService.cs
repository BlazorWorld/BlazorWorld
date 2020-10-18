using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public interface ISecurityService
    {
        Task<bool> AllowedAsync(string loggedInUserId,
            string createdBy,
            string module,
            string type,
            string action);
    }
}
