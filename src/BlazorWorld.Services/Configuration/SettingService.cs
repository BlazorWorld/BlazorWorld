using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Services.Configuration.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Configuration
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMemoryCache _cache;

        public SettingService(ISettingRepository settingRepository,
            IMemoryCache memoryCache)
        {
            _settingRepository = settingRepository;
            _cache = memoryCache;
        }

        public async Task LoadSettingsAsync(IConfiguration configuration)
        {
            var contentAppSettings = new ContentAppSettings();
            configuration.Bind(nameof(ContentAppSettings), contentAppSettings);
            await LoadSettingsAsync(contentAppSettings.PageSizeSettings);
            await LoadSettingsAsync(contentAppSettings.RoleWeightSettings);

            var siteAppSettings = new SiteAppSettings();
            configuration.Bind(nameof(SiteAppSettings), siteAppSettings);
            await LoadSettingsAsync(siteAppSettings.SidebarMenuSettings);

            var securityAppSettings = new SecurityAppSettings();
            configuration.Bind(nameof(SecurityAppSettings), securityAppSettings);
            await LoadSettingsAsync(securityAppSettings.PermissionSettings);
            await LoadSettingsAsync(securityAppSettings.RoleUserSettings);
        }

        private async Task LoadSettingsAsync(Setting[] settings)
        {
            foreach (var setting in settings)
            {
                var existingSetting = await _settingRepository.GetAsync(setting.Id);
                if (existingSetting == null)
                {
                    if (string.IsNullOrEmpty(setting.CreatedDate))
                    {
                        setting.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
                    }
                    _settingRepository.Add(setting);
                }
                else
                {
                    if (!string.IsNullOrEmpty(setting.CreatedDate) && setting.CreatedDate.CompareTo(existingSetting.CreatedDate) > 0)
                    {
                        _settingRepository.Update(setting);
                    }
                }
            }

            await _settingRepository.SaveChangesAsync();
        }

        private async Task<Setting[]> GetByTypeAsync(string type)
        {
            var key = $"Setting:{type}";
            Setting[] settings;
            if (!_cache.TryGetValue(key, out settings))
            {
                settings = await _settingRepository.GetAllByTypeAsync(type);
                _cache.Set(key, settings);
            }

            return settings;
        }

        public async Task<Setting[]> SidebarMenuSettingsAsync() => await GetByTypeAsync("SidebarMenu");

        public async Task<Setting[]> PermissionSettingsAsync() => await GetByTypeAsync("Permission");

        public async Task<Setting[]> RoleUserSettingsAsync() => await GetByTypeAsync("RoleUser");

        public async Task<Setting[]> PageSizeSettingsAsync() => await GetByTypeAsync("PageSize");

        public async Task<Setting[]> RoleWeightSettingsAsync()
        {
            return await GetByTypeAsync("RoleWeight");
        }

        public async Task<bool> AllowedAsync(
            string module,
            string type,
            string action,
            string role)
        {
            bool allowed = false;

            var key = $"Permission:{module}/{type}/{action}/{role}";

            if (!_cache.TryGetValue(key, out allowed))
            {
                var permissions = await PermissionSettingsAsync();
                var permission = permissions.Where(p => p.Key == $"{module}:{type},{action}").FirstOrDefault();
                if (permission != null)
                {
                    var roles = permission.Value.Split(',');
                    allowed = roles.Contains(role);
                }

                _cache.Set(key, allowed);
            }

            return allowed;
        }
    }
}
