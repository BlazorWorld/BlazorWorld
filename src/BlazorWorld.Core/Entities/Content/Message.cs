using BlazorWorld.Core.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Core.Entities.Content
{
    public class Message : Item
    {
        public string Module { get; set; }
        public string GroupId { get; set; }
        [NotMapped]
        public string Username { get; set; }
        public string Content { get; set; }

        public Message()
        {

        }

        public Message(
            string module, 
            string createdBy, 
            string groupId,
            string content)
        {
            this.Module = module;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            this.GroupId = groupId;
            this.Content = content;
        }
    }
}
