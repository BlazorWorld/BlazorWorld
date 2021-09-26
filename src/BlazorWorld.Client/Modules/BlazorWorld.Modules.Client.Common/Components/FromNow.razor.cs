using BlazorWorld.Modules.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Common.Components
{
    public partial class FromNow : ComponentBase
    {
        [Inject]
        public IMomentService MomentService { get; set; }
        [Parameter]
        public string Date { get; set; }
        [Parameter]
        // https://momentjs.com/docs/#/displaying/format/
        public string Format { get; set; } = "LL";
        public string DateText { get; set; }
        public string FromNowDate { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Date))
            {
                DateText = await MomentService.LocalDateAsync(Date, Format);
                FromNowDate = await MomentService.FromNowAsync(Date);
            }
        }
    }
}
