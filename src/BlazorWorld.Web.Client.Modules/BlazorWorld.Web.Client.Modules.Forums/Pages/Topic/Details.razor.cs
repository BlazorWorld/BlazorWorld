using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Modules.Common.Components;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Topic
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebUserService UserService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        [CascadingParameter] 
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Forum Forum { get; set; }
        private Models.Topic Topic { get; set; }
        private string TopicUserName { get; set; }
        private PostsModel Posts { get; set; }
        private bool CanEditTopic { get; set; } = false;
        private bool CanDeleteTopic { get; set; } = false;
        private bool CanAddPost { get; set; } = false;
        private bool CanVote { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var topicNode = await NodeService.GetAsync(Id);
            Topic = Models.Topic.Create(topicNode);
            TopicUserName = !string.IsNullOrEmpty(Topic.CreatedBy) ? await UserService.GetUserNameAsync(Topic.CreatedBy) : string.Empty;
            var forumNode = await NodeService.GetAsync(Topic.ForumId);
            Forum = Models.Forum.Create(forumNode);
            Posts = new PostsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.PostType,
                    ParentId = Topic.Id,
                    OrderBy = new string[]
                    {
                        OrderBy.Hot,
                        OrderBy.Latest
                    },
                    TruncateContent = 140
                }
            };
            await Posts.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditTopic = await SecurityService.AllowedAsync(
                loggedInUserId,
                Topic.CreatedBy,
                Constants.ForumsModule,
                Constants.TopicType,
                Actions.Edit
            );
            CanDeleteTopic = await SecurityService.AllowedAsync(
                loggedInUserId,
                Topic.CreatedBy,
                Constants.ForumsModule,
                Constants.TopicType,
                Actions.Delete
            );
            CanAddPost = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.PostType,
                Actions.Add
            );
            CanVote = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.TopicType,
                Actions.Vote
            );
        }

        public void SavePost(Post post)
        {
            Posts.Add(post, true);
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Id);
            NavigationManager.NavigateTo($"forum/{Forum.Name}");
        }

        public void DeletePost(string Id)
        {
            Posts.Remove(Id);
        }
    }
}
