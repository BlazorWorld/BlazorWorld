using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ISecurityService _securityService;

        public CategoryController(
            ILogger<CategoryController> logger,
            UserManager<ApplicationUser> userManager,
            ICategoryService categoryService,
            ISecurityService securityService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _categoryService = categoryService;
            _securityService = securityService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await _categoryService.GetAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync(string module)
        {
            return Ok(await _categoryService.GetAllAsync(module));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync(string name, string module)
        {
            var category = await _categoryService.GetByNameAsync(name, module);
            return Ok(category != null ? category : new Category());
        }

        [AllowAnonymous]
        [HttpGet("GetBySlug")]
        public async Task<IActionResult> GetBySlugAsync(string slug, string module)
        {
            var category = await _categoryService.GetBySlugAsync(slug, module);
            return Ok(category != null ? category : new Category());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Category category)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var allowed = await _securityService.IsAllowedAsync(
                HttpContext.User,
                category.Module,
                "Category",
                Actions.Add);

            if (!allowed) throw new Exception("User is not allowed to perform the action.");

            category.CreatedBy = user.Id;
            var id = await _categoryService.AddAsync(category);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Category category)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (category.CreatedBy != userId)
            {
                var allowed = await _securityService.IsAllowedAsync(
                    HttpContext.User, category.Module, "Category", Actions.Edit);

                if (!allowed) throw new Exception("User is not allowed to perform the action.");
            }

            category.LastUpdatedBy = userId;
            category.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _categoryService.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryService.GetAsync(id);
            if (category.CreatedBy != userId)
            {
                var moduleAllowed = await _securityService.IsAllowedAsync(
                    HttpContext.User, category.Module, "Category", Actions.Delete);

                var allowed = await _securityService.IsAllowedAsync(
                    HttpContext.User, category.Module, "Category", Actions.Delete);

                if (!moduleAllowed && !allowed) throw new Exception("User is not allowed to perform the action.");
            }

            await _categoryService.DeleteAsync(id);

            return Ok();
        }
    }
}
