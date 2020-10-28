using BlazorWorld.Web.Client.Modules.Profiles.Models;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Profiles.Pages
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
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
            var profile = await NodeService.GetBySlugAsync(
                Constants.ProfilesModule,
                Constants.ProfileType,
                Slug);
            Profile = Models.Profile.Create(profile);
        }

        protected async Task SubmitAsync()
        {
            var contentActivity = new ContentActivity()
            {
                Node = Profile,
                Message = $"Updated the profile: {Profile.Slug}."
            };
            await NodeService.UpdateAsync(contentActivity);
            NavigationManager.NavigateTo($"profile/in/{Profile.Slug}");
        }
    }
}
