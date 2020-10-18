using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorWorld.Services.Security
{
    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public PermissionsService(
            ApplicationDbContext dbContext,
            IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _cache = memoryCache;
        }

        public async Task<bool> AllowedAsync(
            string module,
            string type,
            string action,
            string role,
            bool useCache)
        {
            bool allowed = false;

            var key = $"{module}/{type}/{action}/{role}";

            if (!useCache || !_cache.TryGetValue(key, out allowed))
            {
                var permission = from p in _dbContext.Permissions
                    where (p.Module == module &&
                           p.Type == type &&
                           p.Action == action &&
                           p.Role == role)
                    select p;

                allowed = await permission.AnyAsync();
                if (useCache)
                {
                    _cache.Set(key, allowed);
                }
            }

            return allowed;
        }

        public async Task AddAsync(Permission permission)
        {
            permission.Id = Guid.NewGuid().ToString();
            _dbContext.Permissions.Add(permission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
