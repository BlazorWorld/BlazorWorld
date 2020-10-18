using BlazorWorld.Web.Shared.Models;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public interface IConfigurationService
    {
        Task<SidebarMenuSetting[]> SidebarMenuSettingsAsync();
    }
}
