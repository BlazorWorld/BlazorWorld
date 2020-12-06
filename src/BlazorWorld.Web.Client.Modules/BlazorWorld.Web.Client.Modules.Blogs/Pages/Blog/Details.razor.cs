using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Blogs.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Components;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Blogs.Pages.Blog
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        public Models.Blog Blog { get; set; }
        private bool CanEditBlog { get; set; } = false;
        private bool CanDeleteBlog { get; set; } = false;
        private bool CanAddPost { get; set; } = false;
        private PostsModel Posts { get; set; }
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var blog = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.BlogType,
                Slug);
            Blog = Models.Blog.Create(blog);
            Posts = new PostsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.BlogsModule,
                    Type = Constants.PostType,
                    ParentId = Blog.Id,
                    OrderBy = $"{OrderBy.Weight},{OrderBy.Latest},{OrderBy.Title}",
                    TruncateContent = 140
                }
            };
            await Posts.InitAsync();

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditBlog = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Add
            );
            CanDeleteBlog = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Delete
            );
            CanAddPost = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.BlogsModule,
                Constants.PostType,
                Actions.Add
            );
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Blog.Id);
            NavigationManager.NavigateTo($"/blogs");
        }

    }
}
