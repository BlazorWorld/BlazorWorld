using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Data.Identity;
using BlazorWorld.Services.Content;
using BlazorWorld.Services.Security;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public class ServerCategoryService : IWebCategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ServerCategoryService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ISecurityService _securityService;

        public ServerCategoryService(
            ILogger<ServerCategoryService> logger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            ICategoryService categoryService,
            ISecurityService securityService
            )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _categoryService = categoryService;
            _securityService = securityService;
        }

        public async Task<Category> GetAsync(string id)
        {
            return await _categoryService.GetAsync(id);
        }

        public async Task<Category[]> GetAllAsync(string module)
        {
            return await _categoryService.GetAllAsync(module);
        }

        public async Task<Category> GetByNameAsync(string name, string module)
        {
            var category = await _categoryService.GetByNameAsync(name, module);
            return category != null ? category : new Category();
        }

        public async Task<Category> GetBySlugAsync(string slug, string module)
        {
            var category = await _categoryService.GetBySlugAsync(slug, module);
            return category != null ? category : new Category();
        }

        public async Task<HttpResponseMessage> AddAsync(Category category)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = await _userManager.GetUserAsync(httpContext.User);

            var allowed = await _securityService.IsAllowedAsync(
                httpContext.User,
                category.Module,
                "Category",
                Actions.Add);

            if (!allowed) throw new Exception("User is not allowed to perform the action.");

            category.CreatedBy = user.Id;
            var id = await _categoryService.AddAsync(category);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> UpdateAsync(Category category)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (category.CreatedBy != userId)
            {
                var allowed = await _securityService.IsAllowedAsync(
                    httpContext.User, category.Module, "Category", Actions.Edit);

                if (!allowed) throw new Exception("User is not allowed to perform the action.");
            }

            category.LastUpdatedBy = userId;
            category.LastUpdatedDate = DateTimeOffset.UtcNow.ToString("s");
            await _categoryService.UpdateAsync(category);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            await _categoryService.DeleteAsync(id);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
