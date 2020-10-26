using BlazorWorld.Web.Shared.Models;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebConfigurationService
    {
        Task<SidebarMenuSetting[]> SidebarMenuSettingsAsync();
    }
}
