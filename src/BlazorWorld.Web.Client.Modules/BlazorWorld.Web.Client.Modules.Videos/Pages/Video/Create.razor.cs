using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Videos.Services;
using BlazorWorld.Web.Client.Shell.Services;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages.Video
{
    public partial class Create : ComponentBase
    {
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IVideoService VideoService { get; set; }
        [Parameter]
        public string Slug { get; set; }
        private Models.Video Video { get; set; } = new Models.Video();
        private Models.Channel Channel { get; set; } = new Models.Channel();
        private bool SetAsChannelThumbnail { get; set; } = false;
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override async Task OnParametersSetAsync()
        {
            _editContext = new EditContext(Video);
            _messages = new ValidationMessageStore(_editContext);

            var node = await NodeService.GetBySlugAsync(
                Constants.VideosModule,
                Constants.ChannelType,
                Slug);
            Channel = Models.Channel.Create(node);
            Video.ChannelId = Channel.Id;

            base.OnInitialized();
        }

        protected async Task SubmitAsync()
        {
            await VideoService.SetVideoAttributesAsync(Video);
            var contentActivity = new ContentActivity()
            {
                Node = Video,
                Message = $"Added a new video: {Video.Url}."
            };
            await NodeService.AddAsync(contentActivity);

            if (SetAsChannelThumbnail)
            {
                Channel.ThumbnailUrl = Video.ThumbnailUrl;
                contentActivity = new ContentActivity()
                {
                    Node = Channel,
                    Message = $"Updated a video channel: {Channel.Name}."
                };
                await NodeService.UpdateAsync(contentActivity);
            }
            NavigationManager.NavigateTo($"video/{Video.Id}");
        }

    }
}
