using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Components.Post
{
    public partial class PostForm : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebUserService UserService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string TopicId { get; set; }
        [Parameter]
        public EventCallback<Models.Post> OnSave { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        public Models.Post Post { get; set; } = new Models.Post();
        public string PostUserName { get; set; }
        public string PostUserId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var signedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            if (!string.IsNullOrEmpty(signedInUserId))
            {
                PostUserId = signedInUserId;
                PostUserName = await UserService.GetUserNameAsync(signedInUserId);
            }

            if (!string.IsNullOrEmpty(Id))
            {
                var node = await NodeService.GetAsync(Id);
                Post = Models.Post.Create(node);
            }
        }

        protected async Task SubmitAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Post.TopicId = TopicId;
                var contentActivity = new ContentActivity()
                {
                    Node = Post,
                    Message = $"Added a new forum post."
                };
                await NodeService.AddAsync(contentActivity);
            }
            else
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Post,
                    Message = $"Updated a forum post."
                };
                await NodeService.UpdateAsync(contentActivity);
            }

            await OnSave.InvokeAsync(Post);
        }

        protected async Task CancelAsync()
        {
            await OnCancel.InvokeAsync(null);
        }
    }
}
