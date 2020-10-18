using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Web.Shared.Models
{
    public class SidebarMenuSetting
    {
        public int Order { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public string Permission { get; set; }
        public bool Enabled { get; set; }
    }
}
