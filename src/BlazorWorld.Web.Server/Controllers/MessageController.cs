using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Services.Configuration.Models;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;
        private readonly IGroupService _groupService;
        private readonly IMessageService _messagesService;
        private readonly ContentAppSettings _contentAppSettings;

        public MessageController(
            ILogger<NodeController> logger,
            IGroupService groupService,
            IMessageService messagesService,
            IConfiguration configuration)
        {
            _logger = logger;
            _groupService = groupService;
            _messagesService = messagesService;
            _contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), _contentAppSettings);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string groupId, [FromQuery]int currentPage)
        {
            var group = await _groupService.GetAsync(groupId);
            var module = group.Module;
            var pageSize = PageSize(module);
            return Ok(await _messagesService.GetPaginatedResultAsync(groupId, currentPage, pageSize));
        }

        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCountAsync([FromQuery]string groupId)
        {
            return Ok(await _messagesService.GetCountAsync(groupId));
        }

        [HttpGet("GetPageSize")]
        public IActionResult GetPageSize([FromQuery]string module)
        {
            return Ok(PageSize(module));
        }

        private int PageSize(string module)
        {
            var pageSize = 10;
            var pageSizeSetting = _contentAppSettings.PageSizeSettings.FirstOrDefault(
                pss => pss.Module == module
            );
            if (pageSizeSetting != null) pageSize = pageSizeSetting.PageSize;
            return pageSize;
        }
    }
}
