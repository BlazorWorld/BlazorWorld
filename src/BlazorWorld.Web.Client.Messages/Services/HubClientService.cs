using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Messages.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Messages.Services
{
    public class HubClientService
    {
        public Dictionary<string, MessagesModel> MessagesModels { get; set; }
            = new Dictionary<string, MessagesModel>();
        public delegate void MessageGroupEventHandler(string groupId);
        public event MessageGroupEventHandler OnGroupInit;
        public event MessageGroupEventHandler OnNewMessage;
        private IAccessTokenProvider _tokenProvider;
        private NavigationManager _navigationManager;
        private IGroupService _groupService;
        private IUserApiService _userApiService;
        private HubConnection _hubConnection;
        private IMessageService _messagesService;

        public HubClientService(
            IAccessTokenProvider tokenProvider,
            NavigationManager navigationManager,
            IGroupService groupService,
            IUserApiService userApiService,
            IMessageService messagesService)
        {
            _tokenProvider = tokenProvider;
            _navigationManager = navigationManager;
            _groupService = groupService;
            _userApiService = userApiService;
            _messagesService = messagesService;
        }

        public async Task InitAsync()
        {
            var tokenResult = await _tokenProvider.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token))
            {
                await CreateConnectionAsync();
                var groups = await _groupService.SecureGetAllAsync(string.Empty);
                foreach (var group in groups)
                {
                    await AddToGroupAsync(group.Module, group.Id);
                    if (OnGroupInit != null) OnGroupInit(group.Id);
                }
            }
        }

        private async Task CreateConnectionAsync()
        {
            // https://github.com/dotnet/aspnetcore/issues/25259
            var builder = new HubConnectionBuilder();
            var httpConnectionOptions = HttpConnectionFactoryInternal.CreateHttpConnectionOptions(); // work around constructor call
            httpConnectionOptions.AccessTokenProvider = async () =>
            {
                var accessTokenResult = await _tokenProvider.RequestAccessToken();
                accessTokenResult.TryGetToken(out var accessToken);
                return accessToken.Value;
            };
            httpConnectionOptions.Url = _navigationManager.ToAbsoluteUri(Shared.Constants.MessagesHubPattern);
            builder.Services.AddSingleton<EndPoint>(new UriEndPoint(httpConnectionOptions.Url));
            var opt = Microsoft.Extensions.Options.Options.Create(httpConnectionOptions);
            builder.Services.AddSingleton(opt);
            builder.Services.AddSingleton<IConnectionFactory, HttpConnectionFactoryInternal>();

            _hubConnection = builder.Build();
            _hubConnection.On<Message>("ReceiveMessageAsync", OnReceiveMessageAsync);
            await _hubConnection.StartAsync();
        }

        private async Task OnReceiveMessageAsync(Message message)
        {
            message.Username = await _userApiService.GetUserNameAsync(message.CreatedBy);
            if (MessagesModels.ContainsKey(message.GroupId))
                MessagesModels[message.GroupId].Add(message, false);
            OnNewMessage(message.GroupId);
        }

        private async Task AddToGroupAsync(string module, string groupId)
        {
            await _hubConnection.InvokeAsync("AddToGroupAsync", groupId);
            await InitializeMessagesAsync(module, groupId);
        }

        private async Task InitializeMessagesAsync(string module, string groupId)
        {
            if (!MessagesModels.ContainsKey(groupId))
            {
                var messagesModel = new MessagesModel(_messagesService);
                messagesModel.Module = module;
                messagesModel.GroupId = groupId;
                await messagesModel.InitAsync();

                MessagesModels.Add(groupId, messagesModel);
                if (OnNewMessage != null)
                    OnNewMessage(groupId);
            }
        }

        public Task SendAsync(string module, string groupId, string messageText) =>
                _hubConnection.SendAsync("SendMessageToGroupAsync", module, groupId, messageText);
        
        public bool IsConnected =>
            _hubConnection != null ?
                _hubConnection.State == HubConnectionState.Connected : false;

        public void Dispose()
        {
            _ = _hubConnection.DisposeAsync();
        }
    }
}
