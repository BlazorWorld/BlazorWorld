using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorWorld.Core.Entities.Common
{
    public class Item : Entity
    {
        public string TenantId { get; set; }
        public string SiteId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedDate { get; set; }

        public string FormattedCreatedDate()
        {
            var date = DateTimeOffset.Parse(CreatedDate);
            return date.ToLocalTime().ToString();
        }
        
        public string FormattedUpdatedDate()
        {
            var date = DateTimeOffset.Parse(LastUpdatedDate);
            return date.ToString();
        }

    }
}
