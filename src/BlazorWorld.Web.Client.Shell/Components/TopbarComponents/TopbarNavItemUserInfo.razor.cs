using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Components.TopbarComponents
{
    public partial class TopbarNavItemUserInfo : ComponentBase
    {
        [Inject]
        protected IWebUserService UserService { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> authenticationState { get; set; }
        public string UserId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var user = (await authenticationState).User;
            if (user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == "sub");
                if (userId != null)
                    UserId = userId.Value;
            }
        }
    }
}
