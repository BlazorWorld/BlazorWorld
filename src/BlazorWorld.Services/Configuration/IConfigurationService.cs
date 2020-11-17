using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Shared.Models;

namespace BlazorWorld.Services.Configuration
{
    public interface IConfigurationService
    {
        Setting[] SidebarMenuSettings();
    }
}
