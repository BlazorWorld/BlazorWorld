using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Articles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Articles.Pages.Category
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IWebCategoryService CategoryService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanEditCategory { get; set; } = false;
        private bool CanAddArticle { get; set; } = false;
        private Core.Entities.Content.Category Category { get; set; }
        private ArticlesModel Articles { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Category = await CategoryService.GetBySlugAsync(Slug, Constants.ArticlesModule);

            Articles = new ArticlesModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ArticlesModule,
                    Type = Constants.ArticleType,
                    CategoryId = Category.Id,
                    OrderBy = new string[]
                    {
                            OrderBy.Weight,
                            OrderBy.Latest,
                            OrderBy.Title
                    }
                }
            };
            await Articles.InitAsync();

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditCategory = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.CategoryType,
                Actions.Add
            );
            CanAddArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Add
            );
        }
    }
}
