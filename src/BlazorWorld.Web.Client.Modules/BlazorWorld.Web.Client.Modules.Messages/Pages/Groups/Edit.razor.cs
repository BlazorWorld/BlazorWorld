using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Pages.Groups
{
    [Authorize]
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebGroupService GroupService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        public Models.MessagesGroup Group { get; set; } = new Models.MessagesGroup();
        public string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Group);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            var group = await GroupService.SecureGetAsync(Id);
            Group = Models.MessagesGroup.Create(group);
        }

        protected async Task SubmitAsync()
        {
            await GroupService.PostAsync(Group);
            NavigationManager.NavigateTo($"/messages/group/{Group.Id}");
        }
    }
}
