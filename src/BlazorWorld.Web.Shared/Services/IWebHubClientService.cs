using BlazorWorld.Web.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebHubClientService
    {
        Dictionary<string, MessagesModel> MessagesModels { get; set; }
        delegate void MessageGroupEventHandler(string groupId);
        event MessageGroupEventHandler OnGroupInit;
        event MessageGroupEventHandler OnNewMessage;
        Task InitAsync();
        Task SendAsync(string module, string groupId, string messageText);
        bool IsConnected { get; }
        void Dispose();
    }
}
