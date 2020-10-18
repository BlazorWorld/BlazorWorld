using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Core.Entities.Content
{
    public class EntityTag
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Entity")]
        public string EntityId { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
