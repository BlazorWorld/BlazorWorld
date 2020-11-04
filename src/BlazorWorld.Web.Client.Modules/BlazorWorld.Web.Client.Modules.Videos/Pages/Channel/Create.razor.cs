using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages.Channel
{
    public partial class Create : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        private Models.Channel Channel { get; set; } = new Models.Channel();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Channel);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected async Task SubmitAsync()
        {
            Channel.Slug = Channel.Name.ToSlug();
            var existingChannel = await NodeService.GetBySlugAsync(
                Constants.VideosModule,
                Constants.ChannelType,
                Channel.Slug);

            if (existingChannel == null)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Channel,
                    Message = $"Added a new video channel: {Channel.Name}."
                };
                await NodeService.AddAsync(contentActivity);
                NavigationManager.NavigateTo($"videos/{Channel.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }

    }
}
