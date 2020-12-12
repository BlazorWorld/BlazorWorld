using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Videos.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanEditConfig { get; set; } = false;
        private ChannelsModel Channels { get; set; }
        private Node Config { get; set; } = new Node();
        private bool CanAddChannel { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            var configSearch = new NodeSearch()
            {
                Module = Constants.VideosModule,
                Type = Constants.ConfigType
            };
            var configNodes = (await NodeService.GetAsync(configSearch, 0));
            if (configNodes.Length > 0)
                Config = configNodes[0];
            Channels = new ChannelsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.VideosModule,
                    Type = Constants.ChannelType,
                    OrderBy = $"{OrderBy.Weight},{OrderBy.Latest},{OrderBy.Title}"
                }
            };
            await Channels.InitAsync();

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditConfig = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.VideosModule,
                Constants.ConfigType,
                Actions.Edit
            );
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
