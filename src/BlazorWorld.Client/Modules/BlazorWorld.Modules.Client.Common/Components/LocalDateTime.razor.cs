using BlazorWorld.Modules.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Modules.Client.Common.Components
{
    public partial class LocalDateTime : ComponentBase
    {
        [Inject]
        public IMomentService MomentService { get; set; }
        [Parameter]
        public string Date { get; set; }
        [Parameter]
        public string Format { get; set; } = "LL";
        public string LocalDateTimeString { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Date))
                LocalDateTimeString = await MomentService.LocalDateAsync(Date, Format);
        }
    }
}
