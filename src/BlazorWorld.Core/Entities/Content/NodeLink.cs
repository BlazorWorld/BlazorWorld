using BlazorWorld.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Entities.Content
{
    public class NodeLink : Entity
    {
        [Required]
        public string FromNodeId { get; set; }
        [Required]
        public string ToNodeId { get; set; }
        public string Type { get; set; }
    }
}
