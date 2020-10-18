using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class ContentAppSettings
    {
        public RoleWeightSetting[] RoleWeightSettings { get; set; }
        public PageSizeSetting[] PageSizeSettings { get; set; }
    }
}
