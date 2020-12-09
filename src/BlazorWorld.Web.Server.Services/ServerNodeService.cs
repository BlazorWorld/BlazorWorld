using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Configuration;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerNodeService : IWebNodeService
    {
        private readonly ILogger<ServerNodeService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INodeService _nodeService;
        private readonly IActivityService _activityService;
        private readonly ISecurityService _securityService;
        private readonly ISettingService _settingService;

        public ServerNodeService(
            ILogger<ServerNodeService> logger,
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            INodeService nodeService,
            IActivityService activityService,
            ISecurityService securityService,
            IConfiguration configuration,
            ISettingService configurationService)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _nodeService = nodeService;
            _activityService = activityService;
            _securityService = securityService;
            _settingService = configurationService;
        }

        public async Task<Node> GetAsync(string id)
        {
            return await _nodeService.GetAsync(id);
        }

        private async Task<List<Node>> GetPaginatedResultAsync(NodeSearch nodeSearch,
            int currentPage)
        {
            var pageSize = 10;
            var pageSizeSetting = (await _settingService.PageSizeSettingsAsync()).FirstOrDefault(
                pss => pss.Key == $"{nodeSearch.Module}:{nodeSearch.Type}"
                );
            if (pageSizeSetting != null) pageSize = int.Parse(pageSizeSetting.Value);
            if (nodeSearch.PageSize > 0 && nodeSearch.PageSize < pageSize)
            {
                pageSize = nodeSearch.PageSize;
            }
            return await _nodeService.GetPaginatedResultAsync(nodeSearch, currentPage, pageSize);
        }

        public async Task<Node> GetBySlugAsync(
            string module,
            string type,
            string slug,
            bool noStore = false)
        {
            var nodeSearch = new NodeSearch()
            {
                Module = module,
                Type = type,
                Slug = slug
            };

            var result = (await GetPaginatedResultAsync(nodeSearch, 0)).FirstOrDefault();
            return result != null ? result : new Node();
        }

        public async Task<Node[]> GetAsync(
            NodeSearch nodeSearch,
            int currentPage)
        {
            return (await GetPaginatedResultAsync(nodeSearch, currentPage)).ToArray();
        }

        public async Task<int> GetCountAsync(NodeSearch nodeSearch)
        {
            return await _nodeService.GetCountAsync(nodeSearch);
        }

        public async Task<int> GetPageSizeAsync(NodeSearch nodeSearch)
        {
            var pageSize = 10;
            var pageSizeSetting = (await _settingService.PageSizeSettingsAsync()).FirstOrDefault(
                pss => pss.Key == $"{nodeSearch.Module}:{nodeSearch.Type}"
            );
            if (pageSizeSetting != null) pageSize = int.Parse(pageSizeSetting.Value);
            return pageSize;
        }

        public async Task<Node> SecureGetAsync(string id)
        {
            return await _nodeService.GetAsync(id);
        }

        public async Task<Node> SecureGetAsync(
            string module,
            string type,
            string slug)
        {
            var nodeSearch = new NodeSearch()
            {
                Module = module,
                Type = type,
                Slug = slug
            };

            var result = (await GetPaginatedResultAsync(nodeSearch, 0)).FirstOrDefault();
            return result != null ? result : new Node();
        }

        public async Task<Node[]> SecureGetAsync(
            NodeSearch nodeSearch,
            int currentPage)
        {
            return (await GetPaginatedResultAsync(nodeSearch, currentPage)).ToArray();
        }

        public async Task<int> SecureGetCountAsync(NodeSearch nodeSearch)
        {
            return await _nodeService.GetCountAsync(nodeSearch);
        }

        public async Task<HttpResponseMessage> AddAsync(ContentActivity contentActivity)
        {
            var node = contentActivity.Node;
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (node.CreatedBy != userId)
            {
                var allowed = await _securityService.IsAllowedAsync(
                    httpContext.User, node.Module, node.Type, Actions.Edit);

                if (!allowed) throw new Exception("User is not allowed to perform the action.");
            }

            node.LastUpdatedBy = userId;
            node.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _nodeService.UpdateAsync(node);

            var message = contentActivity.Message;
            await _activityService.AddAsync(node.Id, message, userId);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> UpdateAsync(ContentActivity contentActivity)
        {
            var node = contentActivity.Node;
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (node.CreatedBy != userId)
            {
                var allowed = await _securityService.IsAllowedAsync(
                    httpContext.User, node.Module, node.Type, Actions.Edit);

                if (!allowed) throw new Exception("User is not allowed to perform the action.");
            }

            node.LastUpdatedBy = userId;
            node.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _nodeService.UpdateAsync(node);

            var message = contentActivity.Message;
            await _activityService.AddAsync(node.Id, message, userId);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            var node = await _nodeService.GetAsync(id);
            var httpContext = _httpContextAccessor.HttpContext;

            if (node != null)
            {
                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (node.Id != userId)
                {
                    var allowed = await _securityService.IsAllowedAsync(
                        httpContext.User, node.Module, node.Type, Actions.Delete);

                    if (!allowed) throw new Exception("User is not allowed to perform the action.");
                }

                await _nodeService.DeleteAsync(id);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<string> SecureGetOEmbed(string oEmbedUrl)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, oEmbedUrl);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}
