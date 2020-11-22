using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Services.Configuration;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerConfigurationService : IWebConfigurationService
    {
        private ISettingService _settingService;

        public ServerConfigurationService(ISettingService siteConfigurationService)
        {
            _settingService = siteConfigurationService;
        }

        public async Task<Setting[]> SidebarMenuSettingsAsync()
        {
            return await _settingService.SidebarMenuSettingsAsync();
        }
    }
}
