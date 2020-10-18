using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class PageSizeSetting
    {
        public string Module { get; set; }
        public string Type { get; set; }
        public int PageSize { get; set; }
    }
}
