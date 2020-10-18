using BlazorWorld.Core.Constants;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Modules.Common.Components;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Forums.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Components.Post
{
    public partial class PostView : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected IUserApiService UserService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public bool IsEdit { get; set; } = false;
        [Parameter]
        public EventCallback<string> OnDelete { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private string UserId { get; set; }
        public Models.Post Post { get; set; }
        public string PostUserName { get; set; }
        public CommentsModel Comments { get; set; }
        public bool CanEditPost { get; set; } = false;
        public bool CanDeletePost { get; set; } = false;
        public bool CanAddComment { get; set; } = false;
        public bool CanVote { get; set; } = false;
        public bool ShowComments { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetAsync(Id);
            Post = Models.Post.Create(node);
            PostUserName = !string.IsNullOrEmpty(node.CreatedBy) ? await UserService.GetUserNameAsync(node.CreatedBy) : string.Empty;
            Comments = new CommentsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.ForumsModule,
                    Type = Constants.CommentType,
                    ParentId = Post.Id,
                    OrderBy = new string[]
                    {
                        OrderBy.Hot,
                        OrderBy.Latest
                    }
                }
            };
            await Comments.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditPost = await SecurityService.AllowedAsync(
                loggedInUserId,
                Post.CreatedBy,
                Constants.ForumsModule,
                Constants.PostType,
                Actions.Edit
            );
            CanDeletePost = await SecurityService.AllowedAsync(
                loggedInUserId,
                Post.CreatedBy,
                Constants.ForumsModule,
                Constants.PostType,
                Actions.Delete
            );
            CanAddComment = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.CommentType,
                Actions.Add
            );
            CanVote = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.PostType,
                Actions.Vote
            );
        }

        public void Edit()
        {
            IsEdit = true;
        }

        public void Delete()
        {
            ConfirmModal.Open();
        }

        public async Task DeleteConfirmedAsync()
        {
            await NodeService.DeleteAsync(Id);
            await OnDelete.InvokeAsync(Id);
        }

        public void Save(Models.Post post)
        {
            Post = post;
            IsEdit = false;
        }

        public void Cancel()
        {
            IsEdit = false;
        }

        public void ToggleShowComments()
        {
            ShowComments = !ShowComments;
        }

        public void SaveComment(Models.Comment comment)
        {
            Comments.Add(comment, true);
            Post.ChildCount += 1;
        }

        public void DeleteComment(string Id)
        {
            Comments.Remove(Id);
            Post.ChildCount -= 1;
        }

        public async Task NextCommentsAsync()
        {
            await Comments.NextAsync();
        }
    }
}
