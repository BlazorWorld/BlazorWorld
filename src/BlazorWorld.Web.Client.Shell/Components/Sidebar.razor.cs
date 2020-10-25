using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Shell.Services;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Components
{
    public partial class Sidebar : ComponentBase
    {
        [Inject]
        private IConfiguration Configuration { get; set; }
        [Inject]
        private IConfigurationService ConfigurationService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private Dictionary<string, List<SidebarMenuSetting>> SidebarMenuSettings { get; set; } = new Dictionary<string, List<SidebarMenuSetting>>();

        protected override async Task OnParametersSetAsync()
        {
            SidebarMenuSettings = new Dictionary<string, List<SidebarMenuSetting>>();
            var sidebarMenuSettings = await ConfigurationService.SidebarMenuSettingsAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            foreach (var setting in sidebarMenuSettings)
            {
                var allowed = await SecurityService.AllowedAsync(loggedInUserId, string.Empty,
                    setting.Permission, "Menu", Actions.View);
                if (allowed && setting.Enabled)
                {
                    if (!SidebarMenuSettings.ContainsKey(setting.Category))
                    {
                        SidebarMenuSettings.Add(setting.Category, new List<SidebarMenuSetting>());
                    }

                    if (!SidebarMenuSettings[setting.Category].Any(s => s.Name == setting.Name))
                    {
                        SidebarMenuSettings[setting.Category].Add(setting);
                    }
                }
            }
        }

    }
}
