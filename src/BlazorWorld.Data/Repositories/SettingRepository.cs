﻿using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class SettingRepository : Repository, ISettingRepository
    {
        public SettingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Setting> GetAsync(string id)
        {
            return await _dbContext.Settings.FindAsync(id);
        }

        public async Task<Setting[]> GetAllByTypeAsync(string type)
        {
            return await (from s in _dbContext.Settings
                          where s.Type == type
                          select s)
                          .OrderBy(s => s.Key)
                          .ToArrayAsync();
        }

        public void Add(Setting setting)
        {
            _dbContext.Settings.Add(setting);
        }

        public void Update(Setting setting)
        {
            _dbContext.Settings.Update(setting);
        }

        public void Delete(string id)
        {
            _dbContext.Remove(_dbContext.Settings.Single(s => s.Id == id));
        }
    }
}
