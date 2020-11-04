using BlazorWorld.Web.Client.Messages.Services;
using BlazorWorld.Web.Client.Modules.Messages.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Pages.Groups
{
    [Authorize]
    public partial class Create : ComponentBase
    {
        [Inject]
        protected IWebGroupService GroupService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        private HubClientService HubClientService { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        public Models.MessagesGroup Group { get; set; } = new Models.MessagesGroup();
        private List<string[]> Members { get; set; }
        private string GroupId { get; set; } = string.Empty;
        //private EditContext _editContext;
        //private ValidationMessageStore _messages;

        //protected override void OnInitialized()
        //{
            //_editContext = new EditContext(Group);
            //_messages = new ValidationMessageStore(_editContext);
            //base.OnInitialized();
        //}

        protected async Task OnUpdateUsersAsync(List<string[]> members)
        {
            Group.MemberIds = members.Select(m => m[0]).ToArray();
            Group.CreatedBy = (await AuthenticationStateTask).LoggedInUserId();
            Group.SetKeyFromMemberIds();

            // search if a group exists
            var group = await GroupService.SecureGetByKeyAsync(Group.Key);
            if (!string.IsNullOrEmpty(group.Id))
            {
                Group = MessagesGroup.Create(group);
                GroupId = Group.Id;
            }
        }

        protected async Task CreateGroupAndSendAsync(string message)
        {
            await GroupService.PostAsync(Group);
            await HubClientService.SendAsync(Models.Constants.MessagesModule, Group.Id, message);
            NavigationManager.NavigateTo($"messages/{Group.Id}");
        }
    }
}
