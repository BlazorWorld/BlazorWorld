﻿using BlazorWorld.Web.Shared.Models;

namespace BlazorWorld.Services.Configuration
{
    public interface IConfigurationService
    {
        SidebarMenuSetting[] SidebarMenuSettings();
    }
}