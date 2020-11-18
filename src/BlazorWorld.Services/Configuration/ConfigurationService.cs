using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Web.Shared.Models;
using Microsoft.Extensions.Configuration;

namespace BlazorWorld.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ContentAppSettings _contentAppSettings;
        private readonly SiteAppSettings _siteAppSettings;
        private readonly SecurityAppSettings _securityAppSettings;

        public ConfigurationService(
            IConfiguration configuration)
        {
            _contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), _contentAppSettings);
            _siteAppSettings = new SiteAppSettings();
            configuration.Bind(nameof(SiteAppSettings), _siteAppSettings);
            _securityAppSettings = new SecurityAppSettings();
            configuration.Bind(nameof(SecurityAppSettings), _securityAppSettings);
        }

        public Setting[] SidebarMenuSettings()
        {
            return _siteAppSettings.SidebarMenuSettings;
        }

        public Setting[] PermissionSettings()
        {
            return _securityAppSettings.PermissionSettings;
        }

        public Setting[] RoleUserSettings()
        {
            return _securityAppSettings.RoleUserSettings;
        }

        public Setting[] PageSizeSettings()
        {
            return _contentAppSettings.PageSizeSettings;
        }

        public Setting[] RoleWeightSettings()
        {
            return _contentAppSettings.RoleWeightSettings;
        }

    }
}
