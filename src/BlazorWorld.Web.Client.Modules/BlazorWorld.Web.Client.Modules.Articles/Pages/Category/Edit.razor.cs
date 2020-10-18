using BlazorWorld.Web.Client.Modules.Common;
using BlazorWorld.Web.Client.Modules.Common.Services;
using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Articles.Pages.Category
{
    [Authorize]
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected ICategoryService CategoryService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Core.Entities.Content.Category Category { get; set; } = new Core.Entities.Content.Category();
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
            Category = await CategoryService.GetBySlugAsync(Slug, Constants.ArticlesModule);
        }

        protected async Task SubmitAsync()
        {
            Category.Slug = Category.Name.ToSlug();
            var existingCategory = await CategoryService.GetBySlugAsync(
                Category.Slug,
                Constants.ArticlesModule);

            if (string.IsNullOrEmpty(existingCategory.Id) || existingCategory.Id == Category.Id)
            {
                //var contentActivity = new ContentActivity()
                //{
                //    Node = Article,
                //    Message = $"Added a new category: {Article.Title}."
                //};
                await CategoryService.UpdateAsync(Category);
                NavigationManager.NavigateTo($"articles/{Category.Slug}");
            }
            else
            {
                ValidationMessage = "A similar name already exists.";
            }
        }
    }
}
