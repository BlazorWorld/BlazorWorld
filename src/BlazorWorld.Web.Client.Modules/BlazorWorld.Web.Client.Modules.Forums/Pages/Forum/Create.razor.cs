using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
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
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string ParentId { get; set; }
        private Models.Forum Forum { get; set; } = new Models.Forum();
        private Models.Forum ParentForum { get; set; }
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

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
            _editContext = new EditContext(Forum);
            _messages = new ValidationMessageStore(_editContext);

            if (!string.IsNullOrEmpty(ParentId))
            {
                var node = await NodeService.GetAsync(ParentId);
                if (node != null)
                    ParentForum = Models.Forum.Create(node);
            }

            await base.OnParametersSetAsync();
        }

        protected async Task SubmitAsync()
        {
            Forum.ParentId = ParentId;
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
