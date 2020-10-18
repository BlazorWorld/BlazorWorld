using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Topic
{
    [Authorize]
    public partial class Create : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        private Models.Forum Forum { get; set; } = new Models.Forum();
        private Models.Topic Topic { get; set; } = new Models.Topic();

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ForumsModule,
                Constants.ForumType,
                Slug);
            Forum = Models.Forum.Create(node);
            Topic.ForumId = Forum.Id;
        }

        protected async Task SubmitAsync()
        {
            var contentActivity = new ContentActivity()
            {
                Node = Topic,
                Message = $"Added a new forum topic: {Topic.Snippet}"
            };
            await NodeService.AddAsync(contentActivity);
            NavigationManager.NavigateTo($"forum/topic/{Topic.Id}");
        }
    }
}
