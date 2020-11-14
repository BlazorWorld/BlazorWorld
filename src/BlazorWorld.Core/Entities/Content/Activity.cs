using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BlazorWorld.Core.Entities.Common;

namespace BlazorWorld.Core.Entities.Content
{
    public class Activity : Item
    {
        public string NodeId { get; set; }
        public string Action { get; set; }
        [Required]
        public string Content { get; set; }
        public string GroupId { get; set; }
        public bool IsSystem { get; set; }
    }
}
