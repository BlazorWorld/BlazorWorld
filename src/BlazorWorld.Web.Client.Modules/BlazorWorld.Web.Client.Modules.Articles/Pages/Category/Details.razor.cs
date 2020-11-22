using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Helper;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Articles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Components;
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
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanEditCategory { get; set; } = false;
        private bool CanDeleteCategory { get; set; } = false;
        private bool CanAddArticle { get; set; } = false;
        private Models.Category Category { get; set; }
        private Models.Category ParentCategory { get; set; }
        private bool CanAddCategory { get; set; } = false;
        private Models.Category[] Categories { get; set; }

        private ArticlesModel Articles { get; set; }
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.CategoryType,
                Slug);
            Category = Models.Category.Create(node);

            if (!string.IsNullOrEmpty(Category.ParentId))
            {
                var parentNode = await NodeService.GetAsync(Category.ParentId);
                if (parentNode != null)
                {
                    ParentCategory = Models.Category.Create(parentNode);
                }
            }

            var nodeSearch = new NodeSearch()
            {
                Module = Constants.ArticlesModule,
                Type = Constants.CategoryType,
                ParentId = Category.Id,
                OrderBy = new string[]
                {
                    OrderBy.Weight,
                    OrderBy.Title
                }
            };
            var nodes = await NodeService.GetAsync(nodeSearch, 0);
            Categories = nodes.ConvertTo<Models.Category>();

            Articles = new ArticlesModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ArticlesModule,
                    Type = Constants.ArticleType,
                    ParentId = Category.Id,
                    OrderBy = new string[]
                    {
                            OrderBy.Weight,
                            OrderBy.Latest,
                            OrderBy.Title
                    },
                    TruncateContent = 140
                }
            };
            await Articles.InitAsync();

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddCategory = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.CategoryType,
                Actions.Add
            );
            CanEditCategory = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.CategoryType,
                Actions.Add
            );
            CanDeleteCategory = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.CategoryType,
                Actions.Delete
            );
            CanAddArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Add
            );
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Category.Id);
            NavigationManager.NavigateTo($"/articles");
        }
    }
}
