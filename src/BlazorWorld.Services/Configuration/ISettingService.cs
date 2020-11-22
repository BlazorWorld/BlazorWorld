using BlazorWorld.Core.Entities.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Configuration
{
    public interface ISettingService
    {
        Task LoadSettingsAsync(IConfiguration configuration);
        Task<Setting[]> SidebarMenuSettingsAsync();
        Task<Setting[]> PermissionSettingsAsync();
        Task<Setting[]> RoleUserSettingsAsync();
        Task<Setting[]> PageSizeSettingsAsync();
        Task<Setting[]> RoleWeightSettingsAsync();
        Task<bool> AllowedAsync(
                    string module,
                    string type,
                    string action,
                    string role);
    }
}
