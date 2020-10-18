using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorWorld.Core.Entities.Configuration
{
    public class Permission
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Module { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
