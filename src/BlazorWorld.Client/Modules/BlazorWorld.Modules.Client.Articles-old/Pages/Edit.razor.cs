using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Articles.Pages
{
    public partial class Edit
    {
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Node Config { get; set; } = new Node();
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Config);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            var configSearch = new NodeSearch()
            {
                Module = Constants.ArticlesModule,
                Type = Constants.ConfigType
            };
            var configNodes = (await NodeService.GetAsync(configSearch, 0));
            if (configNodes.Length > 0)
                Config = configNodes[0];
        }

        protected async Task SubmitAsync()
        {
            await NodeService.UpdateAsync(contentActivity);
            NavigationManager.NavigateTo("/articles", true);
        }
    }
}
