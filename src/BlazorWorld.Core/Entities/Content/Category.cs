using BlazorWorld.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorWorld.Core.Entities.Content
{
    public class Category : Item
    {
        public string ParentCategoryId { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
        public string Module { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
    }
}
