using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class RichText : ComponentBase
    {
        [Inject]
        public IMarkdown Markdown { get; set; }
        [Parameter] 
        public string Text { get; set; }
        [Parameter]
        public Object ParentObject { get; set; }
        public string RenderedText { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Text))
                RenderedText = await Markdown.RenderAsync(Text);
        }
    }
}