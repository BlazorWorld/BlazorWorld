using BlazorWorld.Web.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Components
{
    public partial class Created : ComponentBase
    {
        [Parameter]
        public string CreatedBy { get; set; }
        [Parameter]
        public string CreatedDate { get; set; }
    }
}
