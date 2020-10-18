using System;
using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace BlazorWorld.Core.Entities.Content
{
    // https://www.reddit.com/r/programming/comments/bg030/upvotedownvote_database_structure/
    public class NodeVote : Entity
    {
        [Required]
        public string NodeId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public short Score { get; set; }
    }
}
