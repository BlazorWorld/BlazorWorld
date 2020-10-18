using BlazorWorld.Services.Configuration;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWorld.Web.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private ISiteConfigurationService _siteConfigurationService;

        public ConfigurationController(ISiteConfigurationService siteConfigurationService)
        {
            _siteConfigurationService = siteConfigurationService;
        }

        public IActionResult SidebarMenuSettings()
        {
            return Ok(_siteConfigurationService.SidebarMenuSettings());
        }
    }
}
