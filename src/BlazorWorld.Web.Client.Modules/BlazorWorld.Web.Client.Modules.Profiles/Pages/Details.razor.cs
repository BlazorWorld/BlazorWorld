using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Web.Client.Modules.Profiles.Models;
using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Profiles.Pages
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebUserService UserApiService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Profile Profile { get; set; }
        private bool CanEditProfile { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            if (string.IsNullOrEmpty(Slug) && !string.IsNullOrEmpty(loggedInUserId))
            {
                Slug = await UserApiService.GetUserNameAsync(loggedInUserId);
            }
            if (!string.IsNullOrEmpty(Slug))
            {
                var node = await NodeService.GetBySlugAsync(
                Constants.ProfilesModule,
                Constants.ProfileType,
                Slug);
                if (node != null)
                {
                    Profile = Profile.Create(node);
                    var createdBy = node.CreatedBy;
                    CanEditProfile = await SecurityService.AllowedAsync(
                        loggedInUserId,
                        createdBy,
                        Constants.ProfilesModule,
                        Constants.ProfileType,
                        Actions.Add
                    );
                }
                else
                {
                    NavigationManager.NavigateTo("profile/in/new");
                }
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
