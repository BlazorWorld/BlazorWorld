using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerSecurityService : IWebSecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityService _securityService;

        public ServerSecurityService(IHttpContextAccessor httpContextAccessor,
            ISecurityService securityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _securityService = securityService;
        }

        public async Task<bool> AllowedAsync(string loggedInUserId,
            string createdBy,
            string module,
            string type,
            string action)
        {
            if (!string.IsNullOrEmpty(loggedInUserId) && loggedInUserId == createdBy) return true;

            var context = _httpContextAccessor.HttpContext;
            return await _securityService.IsAllowedAsync(context.User, module, type, action);
        }
    }
}
