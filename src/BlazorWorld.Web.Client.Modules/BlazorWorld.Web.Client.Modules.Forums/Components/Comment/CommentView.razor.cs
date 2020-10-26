using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Modules.Common.Components;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Common.Services;
using BlazorWorld.Web.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Components.Comment
{
    public partial class CommentView : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected IWebUserService UserService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public bool IsEdit { get; set; }
        [Parameter]
        public EventCallback<string> OnDelete { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Comment Comment { get; set; }
        private string CommentUserName { get; set; }
        private bool CanEditComment { get; set; } = false;
        private bool CanDeleteComment { get; set; } = false;
        public bool CanVote { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetAsync(Id);
            Comment = Models.Comment.Create(node);
            CommentUserName = await UserService.GetUserNameAsync(node.CreatedBy);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanEditComment = await SecurityService.AllowedAsync(
                loggedInUserId,
                Comment.CreatedBy,
                Constants.ForumsModule,
                Constants.CommentType,
                Actions.Edit
            );
            CanDeleteComment = await SecurityService.AllowedAsync(
                loggedInUserId,
                Comment.CreatedBy,
                Constants.ForumsModule,
                Constants.CommentType,
                Actions.Delete
            );
            CanVote = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.ForumsModule,
                Constants.CommentType,
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

        public void Save(Models.Comment comment)
        {
            Comment = comment;
            IsEdit = false;
        }

        public void Cancel()
        {
            IsEdit = false;
        }

    }
}
