using BlazorWorld.Web.Client.Common.Services;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Components
{
    public partial class Avatar : ComponentBase
    {
        [Inject]
        protected IUserApiService UserApiService { get; set; }
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public string UserId { get; set; }
        [Parameter]
        public int Size { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> authenticationState { get; set; }
        public string AvatarHash { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            AvatarHash = await UserApiService.GetAvatarHashAsync(UserId);
        }
    }
}
