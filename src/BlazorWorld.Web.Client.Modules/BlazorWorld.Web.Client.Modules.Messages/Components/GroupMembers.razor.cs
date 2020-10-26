using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Messages.Components
{
    public partial class GroupMembers : ComponentBase
    {
        [Inject]
        private IWebUserService UserApiService { get; set; }
        [Parameter]
        public EventCallback<List<string[]>> OnUpdateUsers { get; set; }
        [CascadingParameter] 
        EditContext CurrentEditContext { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private ValidationMessageStore _messageStore;
        private string Username { get; set; }
        private List<string[]> Members { get; set; } = new List<string[]>();

        protected override void OnInitialized()
        {
            _messageStore = new ValidationMessageStore(CurrentEditContext);
        }

        protected async Task AddAsync()
        {
            _messageStore.Clear();
            var userId = await UserApiService.GetUserIdAsync(Username);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            if (userId == loggedInUserId)
            {
                _messageStore.Add(CurrentEditContext.Field("Username"), "Adding yourself is not allowed.");
                CurrentEditContext.NotifyValidationStateChanged();
                return;
            }

            if (string.IsNullOrEmpty(userId))
            {
                _messageStore.Add(CurrentEditContext.Field("Username"), $"{Username} not found");
                CurrentEditContext.NotifyValidationStateChanged();
                return;
            }

            if (Members.Any(m => m[0] == userId))
            {
                _messageStore.Add(CurrentEditContext.Field("Username"), $"{Username} already added.");
                CurrentEditContext.NotifyValidationStateChanged();
                return;
            }

            Members.Add(new string[] { userId, Username });
            await OnUpdateUsers.InvokeAsync(Members);
        }
    }
}
