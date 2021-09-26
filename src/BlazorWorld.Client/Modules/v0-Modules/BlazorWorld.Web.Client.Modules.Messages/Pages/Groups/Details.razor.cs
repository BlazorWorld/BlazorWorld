using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Pages.Groups
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected IWebGroupService GroupService { get; set; }
        [Parameter]
        public string Id { get; set; }
        public Models.MessagesGroup Group { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var group = await GroupService.SecureGetAsync(Id);
            Group = Models.MessagesGroup.Create(group);
        }
    }
}
