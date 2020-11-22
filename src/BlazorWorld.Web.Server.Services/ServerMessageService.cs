using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Services.Configuration;
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
        private readonly ISettingService _settingService;

        public ServerMessageService(
            IGroupService groupService,
            IMessageService messagesService,
            ISettingService settingService)
        {
            _groupService = groupService;
            _messagesService = messagesService;
            _settingService = settingService;
        }

        public async Task<Message[]> GetAsync(string groupId, int currentPage)
        {
            var group = await _groupService.GetAsync(groupId);
            var module = group.Module;
            var pageSize = await PageSizeAsync(module, "Message");
            return (await _messagesService.GetPaginatedResultAsync(groupId, currentPage, pageSize)).ToArray();
        }

        public async Task<int> GetCountAsync(string groupId)
        {
            return await _messagesService.GetCountAsync(groupId);
        }

        public async Task<int> GetPageSizeAsync(string module)
        {
            return await PageSizeAsync(module, "Message");
        }

        private async Task<int> PageSizeAsync(string module, string type)
        {
            var pageSize = 10;
            var pageSizeSetting = (await _settingService.PageSizeSettingsAsync()).FirstOrDefault(
                pss => pss.Key == $"{module}:{type}"
            );
            if (pageSizeSetting != null) pageSize = int.Parse(pageSizeSetting.Value);
            return pageSize;
        }
    }
}
