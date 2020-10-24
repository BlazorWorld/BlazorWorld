using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Modules.Videos.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Components
{
    public partial class VideoCard : ComponentBase
    {
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Parameter]
        public Models.Video Video { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanAddVideo { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = Video.CreatedBy;
            CanAddVideo = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.VideosModule,
                Constants.VideoType,
                Actions.Add
            );
        }
    }
}
