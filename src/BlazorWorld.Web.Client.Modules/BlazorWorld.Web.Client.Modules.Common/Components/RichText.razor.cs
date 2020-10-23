using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
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
        private List<Element> Elements { get; set; } = new List<Element>();
        private struct Element
        {
            public string Text;
            public bool IsEmbed;
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var texts = Regex.Split(Text, "({{.*?}})");
                foreach (var text in texts)
                {
                    var element = new Element();
                    var isEmbed = Regex.Match(text, "{{.*?}}");
                    element.IsEmbed = isEmbed.Success;
                    element.Text = isEmbed.Success ? text : await Markdown.RenderAsync(text);
                    Elements.Add(element);
                }
            }
        }
    }
}