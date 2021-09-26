using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [CascadingParameter] 
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanEditConfig { get; set; } = false;
        private bool CanAddForum { get; set; } = false;
        private Node Config { get; set; } = new Node();
        private ForumsModel Forums { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var configSearch = new NodeSearch()
            {
                Module = Constants.ForumsModule,
                Type = Constants.ConfigType
            };
            var configNodes = (await NodeService.GetAsync(configSearch, 0));
            if (configNodes.Length > 0)
                Config = configNodes[0];
            Forums = new ForumsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.ForumType,
                    OrderBy = $"{OrderBy.Title}",
                    RootOnly = true,
                    TruncateContent = 140
                }
            };
            await Forums.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditConfig = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.ConfigType,
                Actions.Edit
            );
            CanAddForum = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.ForumType,
                Actions.Add
            );
        }
    }
}
