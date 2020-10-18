using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Components.Comment
{
    public partial class CommentForm : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected IUserApiService UserService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string PostId { get; set; }
        [Parameter]
        public EventCallback<Models.Comment> OnSave { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private Models.Comment Comment { get; set; } = new Models.Comment();
        private string PreviousComment { get; set; }
        private string SignedInUserId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var node = await NodeService.GetAsync(Id);
                Comment = Models.Comment.Create(node);
            }

            SignedInUserId = (await AuthenticationStateTask).LoggedInUserId();
        }

        protected async Task SubmitAsync()
        {
            if (!string.IsNullOrEmpty(Comment.Content) && 
                Comment.Content != PreviousComment)
            {
                if (Id == null)
                {
                    Comment.PostId = PostId;
                    var contentActivity = new ContentActivity()
                    {
                        Node = Comment,
                        Message = $"Added a new forum comment: {Comment.Snippet}"
                    };
                    await NodeService.AddAsync(contentActivity);
                }
                else
                {
                    var contentActivity = new ContentActivity()
                    {
                        Node = Comment,
                        Message = $"Updated a new forum comment: {Comment.Snippet}"
                    };
                    await NodeService.UpdateAsync(contentActivity);
                }
                await OnSave.InvokeAsync(Comment);

                PreviousComment = Comment.Content;
                Comment.Content = string.Empty;
            }
        }

        protected async Task CancelAsync()
        {
            await OnCancel.InvokeAsync(null);
        }
    }
}
