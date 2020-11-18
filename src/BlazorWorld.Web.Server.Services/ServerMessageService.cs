using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Organization;
using BlazorWorld.Web.Shared;
using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Messages.Services
{
    public class ServerMessageService : IWebMessageService
    {
        private readonly IGroupService _groupService;
        private readonly IMessageService _messagesService;
        private readonly ContentAppSettings _contentAppSettings;

        public ServerMessageService(
            IGroupService groupService,
            IMessageService messagesService,
            IConfiguration configuration)
        {
            _groupService = groupService;
            _messagesService = messagesService;
            _contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), _contentAppSettings);
        }

        public async Task<Message[]> GetAsync(string groupId, int currentPage)
        {
            var group = await _groupService.GetAsync(groupId);
            var module = group.Module;
            var pageSize = PageSize(module, "Message");
            return (await _messagesService.GetPaginatedResultAsync(groupId, currentPage, pageSize)).ToArray();
        }

        public async Task<int> GetCountAsync(string groupId)
        {
            return await _messagesService.GetCountAsync(groupId);
        }

        public async Task<int> GetPageSizeAsync(string module)
        {
            return PageSize(module, "Message");
        }

        private int PageSize(string module, string type)
        {
            var pageSize = 10;
            var pageSizeSetting = _contentAppSettings.PageSizeSettings.FirstOrDefault(
                pss => pss.Key == $"{module}:{type}"
            );
            if (pageSizeSetting != null) pageSize = int.Parse(pageSizeSetting.Value);
            return pageSize;
        }
    }
}
