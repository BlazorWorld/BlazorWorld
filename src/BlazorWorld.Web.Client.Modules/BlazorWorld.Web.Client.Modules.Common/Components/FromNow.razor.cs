using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class FromNow : ComponentBase
    {
        [Inject]
        public IMoment Moment { get; set; }
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
                DateText = await Moment.LocalDateAsync(Date, Format);
                FromNowDate = await Moment.FromNowAsync(Date);
            }
        }
    }
}
