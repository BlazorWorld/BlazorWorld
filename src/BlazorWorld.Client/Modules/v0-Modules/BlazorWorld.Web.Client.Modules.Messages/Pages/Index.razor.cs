using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Messages.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected IWebGroupService GroupService { get; set; }
        private MessagesGroup[] Groups { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var groups = await GroupService.SecureGetAllAsync(Constants.MessagesModule);
            Groups = groups.ConvertTo<MessagesGroup>();
        }
    }
}
