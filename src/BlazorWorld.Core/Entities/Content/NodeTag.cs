using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Core.Entities.Content
{
    public class NodeTag : Entity
    {
        [ForeignKey("Node")]
        public string NodeId { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
