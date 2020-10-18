using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Common.Components;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Forum
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Forum Forum { get; set; }
        private Models.TopicsModel Topics { get; set; }
        private bool CanEditForum { get; set; } = false;
        private bool CanDeleteForum { get; set; } = false;
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
            Topics = new TopicsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.TopicType,
                    ParentId = node.Id,
                    OrderBy = new string[]
                    {
                        OrderBy.Hot,
                        OrderBy.Latest
                    }
                }
            };
            await Topics.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
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
