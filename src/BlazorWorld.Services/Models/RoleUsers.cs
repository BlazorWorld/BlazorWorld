using BlazorWorld.Core.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Services.Configuration.Models
{
    public class RoleUsers
    {
        public string Role { get; set; }
        public string[] Users { get; set; }

        public RoleUsers(Setting setting)
        {
            Role = setting.Key;
            Users = setting.Value.Split(',');
        }
    }
}
