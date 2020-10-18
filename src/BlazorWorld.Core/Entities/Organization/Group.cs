using BlazorWorld.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BlazorWorld.Core.Entities.Organization
{
    public class Group : Item
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string CategoryId { get; set; }
        public string Path { get; set; }
        public bool IsOpen { get; set; }
        public int MemberCount { get; set; }
        [NotMapped]
        public string[] MemberIds { get; set; }

        public void SetKeyFromMemberIds()
        {
            if (MemberIds.Length > 20) Key = string.Empty;
            var memberIds = MemberIds.ToList();
            memberIds = memberIds.Append(CreatedBy).ToList();
            var sortedMemberIds = memberIds.OrderBy(id => id);
            Key = string.Join(",", sortedMemberIds);
        }
    }
}
