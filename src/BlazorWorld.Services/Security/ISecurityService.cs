using BlazorWorld.Core.Entities.Common;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Security
{
    public interface ISecurityService
    {
        Task<bool> IsAdminInConfigAsync(string username);
        Task SetCreatedAsync(Item item, ClaimsPrincipal principal);
        Task SetUpdatedAsync(Item item, ClaimsPrincipal principal);
        Task<bool> IsAllowedAsync(
            ClaimsPrincipal principal, string module, string type, string action);
    }
}
