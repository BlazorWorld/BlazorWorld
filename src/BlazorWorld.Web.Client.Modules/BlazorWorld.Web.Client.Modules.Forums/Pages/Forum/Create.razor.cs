using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Forums.Pages.Forum
{
    [Authorize]
    public partial class Create : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        private Models.Forum Forum { get; set; } = new Models.Forum();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Forum);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected async Task SubmitAsync()
        {
            Forum.Name = Forum.Title.ToSlug();
            var existingForum = await NodeService.GetBySlugAsync(
                Constants.ForumsModule,
                Constants.ForumType,
                Forum.Name);

            if (existingForum == null)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Forum,
                    Message = $"Added a new forum: {Forum.Name}."
                };
                await NodeService.AddAsync(contentActivity);
                NavigationManager.NavigateTo($"forum/{Forum.Slug}");
            }
            else
            {
                ValidationMessage = "A similar title already exists.";
            }
        }
    }
}
