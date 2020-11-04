using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages.Video
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        private Models.Video Video { get; set; }
        private Models.Channel Channel { get; set; }
        private bool CanEditVideo { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetAsync(Id);
            Video = Models.Video.Create(node);
            var channelNode = await NodeService.GetAsync(Video.ParentId);
            Channel = Models.Channel.Create(channelNode);

            base.OnInitialized();
        }
    }
}
