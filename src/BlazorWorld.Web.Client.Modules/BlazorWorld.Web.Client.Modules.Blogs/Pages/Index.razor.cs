using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Blogs.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Blogs.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanAddBlog { get; set; } = false;
        private BlogsModel Blogs { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Blogs = new BlogsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.BlogsModule,
                    Type = Constants.BlogType,
                    OrderBy = new string[]
                    {
                        OrderBy.Title
                    }
                }
            };
            await Blogs.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddBlog = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Add
            );
        }
    }
}
