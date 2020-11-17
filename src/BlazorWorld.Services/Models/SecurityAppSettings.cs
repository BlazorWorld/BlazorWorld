using BlazorWorld.Core.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class SecurityAppSettings
    {
        public Setting[] RoleUserSettings { get; set; }
        public Setting[] PermissionSettings { get; set; }
    }
}
