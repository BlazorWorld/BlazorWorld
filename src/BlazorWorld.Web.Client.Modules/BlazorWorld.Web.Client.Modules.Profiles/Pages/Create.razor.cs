using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Modules.Profiles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Client.Shell.Services;
using BlazorWorld.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Profiles.Pages
{
    public partial class Create : ComponentBase
    {
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected IUserApiService UserApiService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Profile Profile { get; set; } = new Profile();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override async Task OnParametersSetAsync()
        {
            _editContext = new EditContext(Profile);
            _messages = new ValidationMessageStore(_editContext);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            if (!string.IsNullOrEmpty(loggedInUserId))
            {
                Profile.Slug = await UserApiService.GetUserNameAsync(loggedInUserId);
            }
            else
            {
                NavigationManager.NavigateTo($"/");
            }
        }

        protected async Task SubmitAsync()
        {
            var contentActivity = new ContentActivity()
            {
                Node = Profile,
                Message = $"Created the profile: {Profile.Slug}."
            };
            await NodeService.AddAsync(contentActivity);
            NavigationManager.NavigateTo($"profile/in/{Profile.Slug}");
        }
    }
}
