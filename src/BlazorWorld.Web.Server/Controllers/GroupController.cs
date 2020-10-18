using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Services.Organization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(
            IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await _groupService.GetAsync(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string module)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _groupService.GetAllAsync(userId, module));
        }

        [HttpGet("GetByKey")]
        public async Task<IActionResult> GetByKeyAsync([FromQuery] string key)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _groupService.GetByKeyAsync(key) ?? new Group());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Group group)
        {
            group.Id = Guid.NewGuid().ToString();
            group.CreatedBy = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            group.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _groupService.AddAsync(group);

            return Ok(group.Id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Group group)
        {
            group.LastUpdatedBy = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            group.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _groupService.UpdateAsync(group);
            return Ok();
        }

        [HttpGet("GetMembers")]
        public async Task<IActionResult> GroupMembersAsync(string groupId)
        {
            return Ok(await _groupService.GetGroupMembersAsync(groupId));
        }
    }
}