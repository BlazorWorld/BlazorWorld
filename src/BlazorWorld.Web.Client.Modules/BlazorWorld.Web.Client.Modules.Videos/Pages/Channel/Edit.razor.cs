using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Pages.Channel
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        private Models.Channel Channel { get; set; } = new Models.Channel();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override async Task OnParametersSetAsync()
        {
            _editContext = new EditContext(Channel);
            _messages = new ValidationMessageStore(_editContext);

            var node = await NodeService.GetBySlugAsync(
                Constants.VideosModule,
                Constants.ChannelType,
                Slug);
            Channel = Models.Channel.Create(node);

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
                    Message = $"Edited a video channel: {Channel.Name}."
                };
                await NodeService.UpdateAsync(contentActivity);
                NavigationManager.NavigateTo($"videos/{Channel.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }

    }
}
