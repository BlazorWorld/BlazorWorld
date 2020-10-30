using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerVoteService : IWebVoteService
    {
        private readonly ILogger<ServerVoteService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INodeService _nodeService;
        private readonly ISecurityService _securityService;

        public ServerVoteService(
            ILogger<ServerVoteService> logger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            INodeService nodeService,
            ISecurityService securityService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _nodeService = nodeService;
            _securityService = securityService;
        }

        public async Task<NodeVote> GetAsync(string id)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _nodeService.GetVoteAsync(userId, id);
        }

        public async Task<int> AddAsync(string id, bool isUpVote)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var item = await _nodeService.GetAsync(id);
            var allowed = await _securityService.IsAllowedAsync(
                httpContext.User, item.Module, item.Type, Actions.Vote);
            if (!allowed) throw new Exception("User is not allowed to perform the action.");
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _nodeService.VoteAsync(userId, id, isUpVote);
        }
    }
}
