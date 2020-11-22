using BlazorWorld.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorWorld.Core.Entities.Content
{
    public class NodeVersion : Entity
    {
        [Required]
        public string NodeId { get; set; }
        [Required]
        public int Version { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public string AllTags { get; set; }

        public string Snippet
        {
            get
            {
                // strip newline
                string output = this.Content;

                if (!String.IsNullOrEmpty(this.Content))
                {
                    output = Regex.Replace(output, @"\t|\n|\r", " ");

                    // strip tags
                    // output = Markdown.ToHtml(output);
                    output = Regex.Replace(output, @"<[^>]+>|&nbsp;", "").Trim();
                    output = Regex.Replace(output, @"{{[^>]+}}|&nbsp;", "").Trim();

                    // strip extra whitespace
                    output = Regex.Replace(output, @"\s{2,}", " ");

                    if (this.Content.Length > 140)
                    {
                        output = output.Truncate(140);
                        output = output + " ...";
                    }
                }

                return output;
            }
        }
    }
}
