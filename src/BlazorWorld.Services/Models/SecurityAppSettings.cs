using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class SecurityAppSettings
    {
        public RoleUsers[] DefaultRoleUsers { get; set; }
        public string[] DefaultPermissions { get; set; }
    }
}
