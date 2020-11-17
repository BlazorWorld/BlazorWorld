using BlazorWorld.Core.Entities.Configuration;

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

        public SidebarMenuSetting(Setting setting)
        {
            this.Name = setting.Key;
            var fields = setting.Value.Split(',');
            this.Order = int.Parse(fields[0]);
            this.Category = fields[1];
            this.Icon = fields[2];
            this.Link = fields[3];
            this.Permission = fields[4];
            this.Enabled = bool.Parse(fields[5]);
        }
    }
}
