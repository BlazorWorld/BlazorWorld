using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static BlazorWorld.Web.Shared.Services.IWebHubClientService;

namespace BlazorWorld.Web.Client.Modules.Messages.Components
{
    public partial class Chat : ComponentBase
    {
        [Inject]
        ILogger<Chat> Logger { get; set; }
        [Inject]
        private IWebHubClientService HubClientService { get; set; }
        [Inject]
        private IWebMomentService MomentService { get; set; }
        [Parameter]
        public string GroupId { get; set; }
        [Parameter]
        public EventCallback<string> CreateGroupAndSend { get; set; }
        private MessagesModel Messages { get; set; }
        private string message = string.Empty;

        protected override void OnInitialized()
        {
            HubClientService.OnGroupInit += new MessageGroupEventHandler(OnGroupInit);
            HubClientService.OnNewMessage += new MessageGroupEventHandler(OnNewMessage);
            SetMessages();
            base.OnInitialized();
        }

        private void OnGroupInit(string groupId)
         {
            if (groupId == GroupId)
            {
                SetMessages();
                StateHasChanged();
            }
        }

        private void SetMessages()
        {
            if ((HubClientService != null && !string.IsNullOrEmpty(GroupId))
                && HubClientService.MessagesModels.ContainsKey(GroupId))
                Messages = HubClientService.MessagesModels[GroupId];
        }

        private void OnNewMessage(string groupId)
        {
            StateHasChanged();
        }

        private async Task SendAsync()
        {
            if (!string.IsNullOrEmpty(GroupId))
                await HubClientService.SendAsync(Models.Constants.MessagesModule, GroupId, message);
            else
                await CreateGroupAndSend.InvokeAsync(message);
        }
    }
}
