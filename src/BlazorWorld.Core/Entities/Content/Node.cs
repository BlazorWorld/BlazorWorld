﻿using BlazorWorld.Core.Entities.Common;
using BlazorWorld.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorWorld.Core.Entities.Content
{
    public class Node : Item
    {
        [Required]
        public string Module { get; set; }
        [Required]
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; } 

        // Taxonomy Schemes
        public string RootId { get; set; }
        public string ParentId { get; set; }
        public string CategoryId { get; set; }
        public string Path { get; set; }
        public string GroupId { get; set; }
        public string Tags { get; set; }

        // Metrics
        public int Weight { get; set; }
        public int ChildCount { get; set; }
        public int DescendantCount { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public double Hot { get; set; }

        public string Snippet
        {
            get
            {
                return this.Content.Snippet();
            }
        }

        public int Votes
        {
            get
            {
                var votes = UpVotes - DownVotes;
                return votes > 0 ? votes : 0;
            }
        }
    }
}

public static class StringExt
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}