using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Components
{
    public partial class GroupHeader : ComponentBase
    {
        [Inject]
        private IGroupService GroupService { get; set; }
        [Inject]
        private IWebUserService UserApiService { get; set; }
        private string Header { get; set; } = string.Empty;
        [Parameter]
        public Group Group { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Group == null)
                Header = string.Empty;
            else if (!string.IsNullOrEmpty(Group.Name))
                Header = Group.Name;
            else
            {
                var members = await GroupService.SecureGetGroupMembersAsync(Group.Id);
                var maxCount = members.Length > 5 ? 5 : members.Length;
                var subset = new GroupMember[maxCount];
                Array.Copy(members, 0, subset, 0, maxCount);
                var usernames = new List<string>();
                var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
                foreach (var gm in subset)
                {
                    if (gm.UserId != loggedInUserId)
                    {
                        var username = await UserApiService.GetUserNameAsync(gm.UserId);
                        usernames.Add(username);
                    }
                }
                usernames.Sort();
                Header = string.Join(", ", usernames.ToArray());
            }

            await base.OnParametersSetAsync();
        }
    }
}
