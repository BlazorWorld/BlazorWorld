using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Forum
{
    [Authorize]
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        private Models.Forum Forum { get; set; } = new Models.Forum();

        private string Name
        {
            get
            {
                return Forum.Name;
            }
            set
            {
                Forum.Name = value;
                Forum.Slug = value.ToSlug();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ForumsModule,
                Constants.ForumType,
                Slug);
            Forum = Models.Forum.Create(node);
        }

        protected async Task SubmitAsync()
        {
            var contentActivity = new ContentActivity()
            {
                Node = Forum,
                Message = $"Updated a forum: {Forum.Name}."
            };
            await NodeService.UpdateAsync(contentActivity);
            NavigationManager.NavigateTo($"forum/{Forum.Slug}");
        }
    }
}