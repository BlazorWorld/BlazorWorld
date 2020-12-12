using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Helper;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Articles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private bool CanEditConfig { get; set; } = false;
        private bool CanAddCategory { get; set; } = false;
        private Node Config { get; set; } = new Node();
        private Models.Category[] Categories { get; set; }
        private Dictionary<string, ArticlesModel> Articles { get; set; } = new Dictionary<string, ArticlesModel>();

        protected override async Task OnParametersSetAsync()
        {
            var configSearch = new NodeSearch()
            {
                Module = Constants.ArticlesModule,
                Type = Constants.ConfigType
            };
            var configNodes = (await NodeService.GetAsync(configSearch, 0));
            if (configNodes.Length > 0)
                Config = configNodes[0];
            var nodeSearch = new NodeSearch()
            {
                Module = Constants.ArticlesModule,
                Type = Constants.CategoryType,
                OrderBy = $"{OrderBy.Weight},{OrderBy.Title}",
                RootOnly = true
            };
            var nodes = (await NodeService.GetAsync(nodeSearch, 0));
            Categories = nodes.ConvertTo<Models.Category>();

            foreach (var category in Categories)
            {
                var articles = new ArticlesModel(NodeService)
                {
                    NodeSearch = new NodeSearch()
                    {
                        Module = Constants.ArticlesModule,
                        Type = Constants.ArticleType,
                        ParentId = category.Id,
                        OrderBy = $"{OrderBy.Weight},{OrderBy.Latest},{OrderBy.Title}",
                        PageSize = 9,
                        TruncateContent = 140
                    }
                };
                await articles.InitAsync();
                Articles.Add(category.Id, articles);
            }

            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditConfig = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ArticlesModule,
                Constants.ConfigType,
                Actions.Edit
            );
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
