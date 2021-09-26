using BlazorWorld.Web.Client.Shell;
using BlazorWorld.Web.Shared.Models;
using BlazorWorld.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Articles.Pages.Category
{
    [Authorize]
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected IWebNodeService NodeService { get; set; }
        [Inject]
        protected IWebSecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Category Category { get; set; } = new Models.Category();
        private string Name
        {
            get
            {
                return Category.Name;
            }
            set
            {
                Category.Name = value;
                Category.Slug = value.ToSlug();
            }
        }
        private string Weight
        {
            get
            {
                return Category.Weight > 0 ? Category.Weight.ToString() : string.Empty;
            }
            set
            {
                var weight = 0;
                int.TryParse(value, out weight);
                Category.Weight = weight;
            }
        }
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Category);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.CategoryType,
                Slug,
                true);
            Category = Models.Category.Create(node);
        }

        protected async Task SubmitAsync()
        {
            var existingCategory = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.CategoryType,
                Category.Slug,
                true);

            if (existingCategory == null || existingCategory.Id == Category.Id)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Category,
                    Message = $"Added a new article category: {Category.Name}."
                };
                await NodeService.UpdateAsync(contentActivity);
                NavigationManager.NavigateTo($"articles/{Category.Slug}", true);
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }
    }
}
