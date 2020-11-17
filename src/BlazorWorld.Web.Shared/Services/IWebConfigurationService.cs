using BlazorWorld.Core.Entities.Configuration;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebConfigurationService
    {
        Task<Setting[]> SidebarMenuSettingsAsync();
    }
}
