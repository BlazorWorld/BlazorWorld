using BlazorWorld.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class LocalDateTime : ComponentBase
    {
        [Inject]
        public IMoment Moment { get; set; }
        [Parameter]
        public string Date { get; set; }
        [Parameter]
        public string Format { get; set; } = "LL";
        public string LocalDateTimeString { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Date))
                LocalDateTimeString = await Moment.LocalDateAsync(Date, Format);
        }
    }
}
