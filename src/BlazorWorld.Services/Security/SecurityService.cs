using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Common;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Configuration;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISettingService _settingService;

        public SecurityService(
            UserManager<ApplicationUser> userManager,
            ISettingService settingService
            )
        {
            _userManager = userManager;
            _settingService = settingService;
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

        public async Task<bool> IsAdminInConfigAsync(string username)
        {
            Console.WriteLine("RoleUserSettings Count: " + (await _settingService.RoleUserSettingsAsync()).Count());
            var admin = (from ru in await _settingService.RoleUserSettingsAsync()
                         where ru.Key == Roles.Admin
                         select ru.Value).FirstOrDefault();
            var adminUsers = admin.Split(',');
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

            if (principal == null) return false;

            var user = await _userManager.GetUserAsync(principal);

            if (user == null) return false;

            if (await IsAllowedForRegisteredUsersAsync(
                module,
                type,
                action)) return true;

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var allowed = await _settingService.AllowedAsync(module, type, action, role);
                if (allowed) return true;
            }

            return false;
        }

        private async Task<bool> IsAllowedForEveryoneAsync(
            string module,
            string type,
            string action)
        {
            return await _settingService.AllowedAsync(module, type, action, Roles.Guest);
        }

        private async Task<bool> IsAllowedForRegisteredUsersAsync(
            string module,
            string type,
            string action)
        {
            return await _settingService.AllowedAsync(
                module, type, action, Roles.User);
        }
    }
}
