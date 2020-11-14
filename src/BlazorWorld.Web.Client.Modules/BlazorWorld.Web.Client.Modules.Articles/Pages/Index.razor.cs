using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Helper;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Articles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Articles.Pages
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
        private bool CanAddCategory { get; set; } = false;
        private Models.Category[] Categories { get; set; }
        private Dictionary<string, ArticlesModel> Articles { get; set; } = new Dictionary<string, ArticlesModel>();

        protected override async Task OnParametersSetAsync()
        {
            var nodeSearch = new NodeSearch()
            {
                Module = Constants.ArticlesModule,
                Type = Constants.CategoryType,
                OrderBy = new string[]
                {
                    OrderBy.Title
                }
            };
            var nodes = (await NodeService.GetAsync(nodeSearch, 0))
                .OrderBy(a => a.Weight)
                .ThenBy(a => a.Title);
            Categories = nodes.ToArray().ConvertTo<Models.Category>();

            foreach (var category in Categories)
            {
                var articles = new ArticlesModel(NodeService)
                {
                    NodeSearch = new NodeSearch()
                    {
                        Module = Constants.ArticlesModule,
                        Type = Constants.ArticleType,
                        ParentId = category.Id,
                        OrderBy = new string[]
                        {
                            OrderBy.Weight,
                            OrderBy.Latest,
                            OrderBy.Title
                        },
                        PageSize = 9,
                        TruncateContent = 140
                    }
                };
                await articles.InitAsync();
                Articles.Add(category.Id, articles);
            }

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddCategory = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.CategoryType,
                Actions.Add
            );
        }
    }
}
