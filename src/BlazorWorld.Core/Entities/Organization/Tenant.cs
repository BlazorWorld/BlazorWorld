using BlazorWorld.Core.Entities.Common;

namespace BlazorWorld.Core.Entities.Organization
{
    public class Tenant : Entity
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Description { get; set; }
    }
}
