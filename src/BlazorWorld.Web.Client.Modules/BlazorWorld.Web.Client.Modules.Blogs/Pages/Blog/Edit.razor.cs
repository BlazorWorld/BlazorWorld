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
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
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

        protected override async Task OnParametersSetAsync()
        {
            var blog = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.BlogType,
                Slug);
            Blog = Models.Blog.Create(blog);
        }

        protected async Task SubmitAsync()
        {
            Blog.Slug = Blog.Name.ToSlug();
            var existingBlog = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.BlogType,
                Blog.Slug);

            if (existingBlog == null || existingBlog.Id == Blog.Id)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Blog,
                    Message = $"Updated blog: {Blog.Name}."
                };
                await NodeService.UpdateAsync(contentActivity);
                NavigationManager.NavigateTo($"blog/{Blog.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }
    }
}
