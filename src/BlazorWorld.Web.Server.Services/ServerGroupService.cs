using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Services.Organization;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerGroupService : IWebGroupService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupService _groupService;

        public ServerGroupService(
            IHttpContextAccessor httpContextAccessor,
            IGroupService groupService)
        {
            _httpContextAccessor = httpContextAccessor;
            _groupService = groupService;
        }

        public async Task<Group> SecureGetAsync(string id)
        {
            return await _groupService.GetAsync(id);
        }

        public async Task<Group[]> SecureGetAllAsync(string module)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _groupService.GetAllAsync(userId, module);
        }

        public async Task<Group> SecureGetByKeyAsync(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _groupService.GetByKeyAsync(key) ?? new Group();
        }

        public async Task<GroupMember[]> SecureGetGroupMembersAsync(string groupId)
        {
            return await _groupService.GetGroupMembersAsync(groupId);
        }

        public async Task<HttpResponseMessage> PostAsync(Group group)
        {
            group.Id = Guid.NewGuid().ToString();
            var httpContext = _httpContextAccessor.HttpContext;
            group.CreatedBy = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            group.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _groupService.AddAsync(group);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutAsync(Group group)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            group.LastUpdatedBy = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            group.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _groupService.UpdateAsync(group);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
