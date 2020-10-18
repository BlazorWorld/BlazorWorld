using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Common;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Configuration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionsService _permissionsService;

        public SecurityService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IPermissionsService permissionsService
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _permissionsService = permissionsService;
        }

        public async Task SetCreatedAsync(Item item, ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            item.CreatedBy = user.Id;
            item.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
        }

        public async Task SetUpdatedAsync(Item item, ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            item.LastUpdatedBy = user.Id;
            item.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
        }

        public bool IsAdminInConfig(string username)
        {
            var securityAppSettings = new SecurityAppSettings();
            _configuration.Bind(nameof(SecurityAppSettings), securityAppSettings);
            var adminUsers = (from ru in securityAppSettings.DefaultRoleUsers
                             where ru.Role == Roles.Admin
                             select ru.Users).FirstOrDefault();
            return adminUsers.Contains(username);
        }

        public async Task<bool> IsAllowedAsync(
            ClaimsPrincipal principal, 
            string module,
            string type,
            string action)
        {
            if (await IsAllowedForEveryoneAsync(
                module,
                type,
                action)) return true;

            var user = await _userManager.GetUserAsync(principal);

            if (user == null) return false;

            if (await IsAllowedForRegisteredUsersAsync(
                module,
                type,
                action)) return true;

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var allowed = await _permissionsService.AllowedAsync(module, type, action, role, true);
                if (allowed) return true;
            }

            return false;
        }

        private async Task<bool> IsAllowedForEveryoneAsync(
            string module,
            string type,
            string action)
        {
            return await _permissionsService.AllowedAsync(module, type, action, Roles.Guest, true);
        }

        private async Task<bool> IsAllowedForRegisteredUsersAsync(
            string module,
            string type,
            string action)
        {
            return await _permissionsService.AllowedAsync(
                module, type, action, Roles.User, true);
        }
    }
}
