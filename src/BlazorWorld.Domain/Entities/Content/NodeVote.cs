using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Domain.Entities.Content
{
    // https://www.reddit.com/r/programming/comments/bg030/upvotedownvote_database_structure/
    public class NodeVote : AuditableEntity<string>
    {
        [ForeignKey("Node")]
        public string NodeId { get; set; }
        public Node Node { get; set; }
        [Required]
        public short Score { get; set; }
    }
}
