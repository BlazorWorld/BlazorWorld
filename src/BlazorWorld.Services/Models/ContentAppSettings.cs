using BlazorWorld.Core.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class ContentAppSettings
    {
        public Setting[] RoleWeightSettings { get; set; }
        public Setting[] PageSizeSettings { get; set; }
    }
}
