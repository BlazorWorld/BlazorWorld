using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Services.Content;
using BlazorWorld.Web.Server.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Hubs
{
    [Authorize]
    public class MessagesHub : Hub<IMessagesClient>
    {
        private readonly IMessageService _messagesService;

        public MessagesHub(IMessageService messagesService)
        {
            _messagesService = messagesService;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-3.1#strongly-typed-hubs
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string module, string messageText)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = new Message(module, userId, string.Empty, messageText);
            await _messagesService.SaveAsync(message);
            await Clients.All.ReceiveMessageAsync(message);
        }

        public async Task SendMessageToCallerAsync(string module, string messageText)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = new Message(module, userId, string.Empty, messageText);
            //await _messagesService.Save(message);
            await Clients.Caller.ReceiveMessageAsync(message);
        }

        //public async Task SendPrivateMessage(
        //    string organization,
        //    string module,
        //    string toUserId,
        //    string messageText)
        //{
        //    var fromUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    find or create new room
        //     var message = new Message(organization, module, fromUserId, toUserId, string.Empty, messageText);
        //    await _messagesService.Save(message);
        //    await Clients.User(toUserId).ReceiveMessage(message);
        //}

        public async Task AddToGroupAsync(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroupAsync(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendMessageToGroupAsync(
            string module, 
            string groupId,
            string messageText)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = new Message(module, userId, groupId, messageText);
            await _messagesService.SaveAsync(message);
            await Clients.Group(groupId).ReceiveMessageAsync(message);
        }
    }
}