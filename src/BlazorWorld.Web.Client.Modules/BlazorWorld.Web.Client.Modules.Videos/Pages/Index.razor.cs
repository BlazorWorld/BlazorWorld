using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Videos.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private ChannelsModel Channels { get; set; }
        private bool CanAddChannel { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            Channels = new ChannelsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.VideosModule,
                    Type = Constants.ChannelType,
                    OrderBy = new string[]
                    {
                            OrderBy.Weight,
                            OrderBy.Latest,
                            OrderBy.Title
                    }
                }
            };
            await Channels.InitAsync();

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddChannel = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.VideosModule,
                Constants.ChannelType,
                Actions.Add
            );
        }
    }
}
