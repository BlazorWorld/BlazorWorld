using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Shell.Services;
using BlazorWorld.Web.Shared;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Blogs.Pages.Post
{
    [Authorize]
    public partial class Create : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Blog Blog { get; set; }
        private Models.Post Post { get; set; } = new Models.Post();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Post);
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
            Post.BlogId = Blog.Id;
        }

        protected async Task SubmitAsync()
        {
            Post.Slug = Post.Title.ToSlug();
            var existingPost = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.PostType,
                Post.Slug);

            if (existingPost == null)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Post,
                    Message = $"Added a new post: {Post.Title}."
                };
                await NodeService.AddAsync(contentActivity);
                NavigationManager.NavigateTo($"blog/post/{Post.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }
    }
}
