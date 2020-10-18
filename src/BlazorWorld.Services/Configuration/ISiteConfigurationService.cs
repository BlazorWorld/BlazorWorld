using BlazorWorld.Web.Shared.Models;

namespace BlazorWorld.Services.Configuration
{
    public interface ISiteConfigurationService
    {
        SidebarMenuSetting[] SidebarMenuSettings();
    }
}
