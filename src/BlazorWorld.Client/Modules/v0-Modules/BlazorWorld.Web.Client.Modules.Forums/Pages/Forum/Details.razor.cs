using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Components;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Forum
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
        private Models.Forum Forum { get; set; }
        private Models.Forum ParentForum { get; set; }
        private ForumsModel Forums { get; set; }
        private Models.TopicsModel Topics { get; set; }
        private bool CanEditForum { get; set; } = false;
        private bool CanDeleteForum { get; set; } = false;
        private bool CanAddForum { get; set; } = false;
        private bool CanAddTopic { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ForumsModule,
                Constants.ForumType,
                Slug);
            Forum = Models.Forum.Create(node);
            var createdBy = node.CreatedBy;

            if (!string.IsNullOrEmpty(Forum.ParentId))
            {
                var parentNode = await NodeService.GetAsync(Forum.ParentId);
                if (parentNode != null)
                    ParentForum = Models.Forum.Create(parentNode);
            }
            else
            {
                ParentForum = null;
            }

            Forums = new ForumsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.ForumType,
                    OrderBy = $"{OrderBy.Title}",
                    ParentId = node.Id
                }
            };
            await Forums.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddForum = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.ForumType,
                Actions.Add
            );
            Topics = new TopicsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.TopicType,
                    ParentId = node.Id,
                    OrderBy = $"{OrderBy.Hot},{OrderBy.Latest}",
                    TruncateContent = 140
                }
            };
            await Topics.InitAsync();
            CanEditForum = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ForumsModule,
                Constants.ForumType,
                Actions.Edit
            );
            CanDeleteForum = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ForumsModule,
                Constants.ForumType,
                Actions.Delete
            );
            CanAddTopic = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.TopicType,
                Actions.Add
            );
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Forum.Id);
            NavigationManager.NavigateTo($"forums");
        }
    }
}
