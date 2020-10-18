using BlazorWorld.Web.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class Username : ComponentBase
    {
        [Inject]
        protected IUserApiService UserApiService { get; set; }
        [Parameter]
        public string UserId { get; set; }
        private string UsernameText { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(UserId))
                UsernameText = await UserApiService.GetUserNameAsync(UserId);
        }
    }
}
