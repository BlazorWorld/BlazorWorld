using BlazorWorld.Services.Configuration;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private ISettingService _settingService;

        public ConfigurationController(ISettingService siteConfigurationService)
        {
            _settingService = siteConfigurationService;
        }

        public async Task<IActionResult> SidebarMenuSettings()
        {
            return Ok(await _settingService.SidebarMenuSettingsAsync());
        }
    }
}
