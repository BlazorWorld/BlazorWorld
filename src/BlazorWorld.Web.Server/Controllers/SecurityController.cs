using BlazorWorld.Services.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<IActionResult> AllowedAsync(string module, string type, string action)
        {
            var result = await _securityService.IsAllowedAsync(HttpContext.User,
                module, type, action);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GuestAllowed")]
        public async Task<IActionResult> PublicAllowedAsync(string module, string type, string action)
        {
            var result = await _securityService.IsAllowedAsync(null,
                module, type, action);
            return Ok(result);
        }
    }
}