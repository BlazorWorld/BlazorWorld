using BlazorWorld.Services.Configuration;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Shell.Services
{
    public class ServerConfigurationService : IWebConfigurationService
    {
        private IConfigurationService _siteConfigurationService;

        public ServerConfigurationService(IConfigurationService siteConfigurationService)
        {
            _siteConfigurationService = siteConfigurationService;
        }

        public async Task<SidebarMenuSetting[]> SidebarMenuSettingsAsync()
        {
            return _siteConfigurationService.SidebarMenuSettings();
        }
    }
}
