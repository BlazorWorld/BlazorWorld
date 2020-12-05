using BlazorWorld.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Entities.Organization
{
    public class Email : Entity
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string DateSent { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
    }
}
