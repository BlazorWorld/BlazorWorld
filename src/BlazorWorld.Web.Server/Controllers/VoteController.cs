using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : Controller
    {
        private readonly ILogger<NodeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INodeService _nodeService;
        private readonly ISecurityService _securityService;

        public VoteController(
            ILogger<NodeController> logger,
            UserManager<ApplicationUser> userManager,
            INodeService nodeService,
            ISecurityService securityService)
        {
            _logger = logger;
            _userManager = userManager;
            _nodeService = nodeService;
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string itemId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _nodeService.GetVoteAsync(userId, itemId));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(VoteAction voteAction)
        {
            var item = await _nodeService.GetAsync(voteAction.ItemId);
            var allowed = await _securityService.IsAllowedAsync(
                HttpContext.User, item.Module, item.Type, Actions.Vote);
            if (!allowed) throw new Exception("User is not allowed to perform the action.");
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _nodeService.VoteAsync(userId, voteAction.ItemId, voteAction.IsUpVote));
        }
    }
}
