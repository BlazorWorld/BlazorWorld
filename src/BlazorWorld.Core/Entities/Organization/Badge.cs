using System;
using System.Collections.Generic;
using System.Text;
using BlazorWorld.Core.Entities.Common;

namespace BlazorWorld.Core.Entities.Organization
{
    public class Badge : Item
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
