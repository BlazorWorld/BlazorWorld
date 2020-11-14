using BlazorWorld.Web.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorWorld.Web.Shared.Services.IWebHubClientService;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerHubClientService : IWebHubClientService
    {
        public Dictionary<string, MessagesModel> MessagesModels { get; set; }
            = new Dictionary<string, MessagesModel>();
        public event MessageGroupEventHandler OnGroupInit;
        public event MessageGroupEventHandler OnNewMessage;

        public async Task InitAsync()
        {
        }

        public async Task SendAsync(string module, string groupId, string messageText)
        {

        }

        public bool IsConnected => false;

        public void Dispose()
        {
        }
    }
}
