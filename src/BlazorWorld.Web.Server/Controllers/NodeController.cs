using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INodeService _nodeService;
        private readonly IActivityService _activityService;
        private readonly ISecurityService _securityService;
        private readonly ContentAppSettings _contentAppSettings;

        public NodeController(
            ILogger<NodeController> logger,
            UserManager<ApplicationUser> userManager,
            INodeService nodeService,
            IActivityService activityService,
            ISecurityService securityService,
            IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _nodeService = nodeService;
            _activityService = activityService;
            _securityService = securityService;
            _contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), _contentAppSettings);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await _nodeService.GetAsync(id));
        }

        [AllowAnonymous]
        [HttpPost("GetPaginatedResult")]
        public async Task<IActionResult> GetPaginatedResultAsync(
            [FromBody]NodeSearch nodeSearch,
            int currentPage)
        {
            var pageSize = 10;
            var pageSizeSetting = _contentAppSettings.PageSizeSettings.FirstOrDefault(
                pss => pss.Module == nodeSearch.Module &&
                       pss.Type == nodeSearch.Type
                );
            if (pageSizeSetting != null) pageSize = pageSizeSetting.PageSize;
            if (nodeSearch.PageSize > 0 && nodeSearch.PageSize < pageSize)
            {
                pageSize = nodeSearch.PageSize;
            }
            return Ok(await _nodeService.GetPaginatedResultAsync(nodeSearch, currentPage, pageSize));
        }

        [AllowAnonymous]
        [HttpPost("GetPageSize")]
        public IActionResult GetPageSize(
            [FromBody]NodeSearch nodeSearch)
        {
            var pageSize = 10;
            var pageSizeSetting = _contentAppSettings.PageSizeSettings.FirstOrDefault(
                pss => pss.Module == nodeSearch.Module &&
                       pss.Type == nodeSearch.Type
            );
            if (pageSizeSetting != null) pageSize = pageSizeSetting.PageSize;
            return Ok(pageSize);
        }

        [AllowAnonymous]
        [HttpPost("GetCount")]
        public async Task<IActionResult> GetCountAsync([FromBody]NodeSearch nodeSearch)
        {
            return Ok(await _nodeService.GetCountAsync(nodeSearch));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ContentActivity contentActivity)
        {
            var node = contentActivity.Node;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allowed = await _securityService.IsAllowedAsync(
                HttpContext.User,
                node.Module,
                node.Type,
                Actions.Add);

            if (!allowed) throw new Exception("User is not allowed to perform the action.");
                
            node.CreatedBy = userId;
            var id = await _nodeService.AddAsync(node);
            var message = contentActivity.Message;
            await _activityService.AddAsync(id, message, userId);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(ContentActivity contentActivity)
        {
            var node = contentActivity.Node;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (node.CreatedBy != userId)
            {
                var allowed = await _securityService.IsAllowedAsync(
                    HttpContext.User, node.Module, node.Type, Actions.Edit);

                if (!allowed) throw new Exception("User is not allowed to perform the action.");
            }

            node.LastUpdatedBy = userId;
            node.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _nodeService.UpdateAsync(node);

            var message = contentActivity.Message;
            await _activityService.AddAsync(node.Id, message, userId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var node = await _nodeService.GetAsync(id);

            if (node != null)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (node.Id != userId)
                {
                    var allowed = await _securityService.IsAllowedAsync(
                        HttpContext.User, node.Module, node.Type, Actions.Delete);

                    if (!allowed) throw new Exception("User is not allowed to perform the action.");
                }

                await _nodeService.DeleteAsync(id);
            }

            return Ok();
        }
    }
}
