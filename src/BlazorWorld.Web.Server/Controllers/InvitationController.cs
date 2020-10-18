using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IInvitationService _invitationService;

        public InvitationController(
            IConfiguration configuration,
            IInvitationService invitationService
            )
        {
            _configuration = configuration;
            _invitationService = invitationService;
        }

        [HttpPost]
        public async Task<IActionResult> Invite(Invitation invitation)
        {
            var url = _configuration["LoginUrl"];
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _invitationService.AddAsync(url, userId, invitation.Email);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var invitations = await _invitationService.GetInvitationsAsync(userId);

            return Ok(invitations);
        }
    }
}
