using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
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
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Parameter]
        public string Path { get; set; }
        [CascadingParameter] 
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanAddForum { get; set; } = false;
        private ForumsModel Forums { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Forums = new ForumsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.ForumType,
                    Path = Path,
                    OrderBy = new string[]
                    {
                        OrderBy.Title
                    },
                    RootOnly = true
                }
            };
            await Forums.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
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
