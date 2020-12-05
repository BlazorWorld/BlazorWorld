using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Core.Entities.Content
{
    public class NodeReaction : Entity
    {
        [ForeignKey("Node")]
        // Applies to Activities, Messages, and Nodes
        public string NodeId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ReactionType { get; set; }
    }
}
