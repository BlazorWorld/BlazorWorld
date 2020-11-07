using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Modules.Common.Components;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Video Video { get; set; }
        private Models.Channel Channel { get; set; }
        private bool CanEditVideo { get; set; } = false;
        private bool CanDeleteVideo { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetAsync(Id);
            Video = Models.Video.Create(node);
            var channelNode = await NodeService.GetAsync(Video.ParentId);
            Channel = Models.Channel.Create(channelNode);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = node.CreatedBy;
            CanEditVideo = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.VideosModule,
                Constants.VideoType,
                Actions.Edit
            );
            CanDeleteVideo = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.VideosModule,
                Constants.VideoType,
                Actions.Delete
            );

            base.OnInitialized();
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Channel.Id);
            NavigationManager.NavigateTo($"/videos/{Channel.Slug}");
        }
    }
}
