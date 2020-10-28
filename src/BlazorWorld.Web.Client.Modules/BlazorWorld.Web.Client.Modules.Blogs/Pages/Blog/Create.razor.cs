using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Blogs.Pages.Blog
{
    [Authorize]
    public partial class Create : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Blog Blog { get; set; } = new Models.Blog();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Blog);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected async Task SubmitAsync()
        {
            Blog.Slug = Blog.Name.ToSlug();
            var existingArticle = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.BlogType,
                Blog.Slug);

            if (existingArticle == null)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Blog,
                    Message = $"Added a new blog: {Blog.Name}."
                };
                await NodeService.AddAsync(contentActivity);
                NavigationManager.NavigateTo($"blog/{Blog.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }
    }
}
