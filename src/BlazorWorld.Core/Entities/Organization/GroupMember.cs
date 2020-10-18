using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace BlazorWorld.Core.Entities.Organization
{
    public class GroupMember : Item
    {
        [Required]
        public string GroupId { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
