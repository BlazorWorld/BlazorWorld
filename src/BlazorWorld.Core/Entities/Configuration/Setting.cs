﻿using BlazorWorld.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace BlazorWorld.Core.Entities.Configuration
{
    public class Setting : Entity
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public string CreatedDate { get; set; }
    }
}
