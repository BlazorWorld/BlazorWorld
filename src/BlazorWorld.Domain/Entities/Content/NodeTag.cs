using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Domain.Entities.Content
{
    public class NodeTag : AuditableEntity<string>
    {
        [ForeignKey("Node")]
        public string NodeId { get; set; }
        public Node Node { get; set; }
        public string Tag { get; set; }
    }
}
