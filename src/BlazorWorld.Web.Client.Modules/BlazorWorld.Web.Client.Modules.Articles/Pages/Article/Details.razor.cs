using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Components;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Articles.Pages.Article
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Article Article { get; set; }
        private Models.Category Category { get; set; }
        private bool CanEditArticle { get; set; } = false;
        private bool CanDeleteArticle { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.ArticleType,
                Slug);
            Article = Models.Article.Create(node);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = node.CreatedBy;
            CanEditArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Edit
            );
            CanDeleteArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Delete
            );
            var catgoryNode = await NodeService.GetAsync(Article.CategoryId);
            Category = Models.Category.Create(node);
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Article.Id);
            NavigationManager.NavigateTo($"/articles/{Category?.Slug}");
        }
    }
}