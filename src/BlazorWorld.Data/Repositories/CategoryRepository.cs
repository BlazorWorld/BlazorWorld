using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Category> GetAsync(string id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category[]> GetAllAsync(string module)
        {
            var categories = from c in _dbContext.Categories
                             where c.Module == module
                             select c;
            return await categories.ToArrayAsync<Category>();
        }

        public async Task<Category> GetByNameAsync(string name, string module)
        {
            var category = from c in _dbContext.Categories
                           where c.Name == name && c.Module == module
                           select c;
            return await category.FirstOrDefaultAsync<Category>();
        }

        public async Task<Category> GetBySlugAsync(string slug, string module)
        {
            var category = from c in _dbContext.Categories
                           where c.Slug == slug && c.Module == module
                           select c;
            return await category.FirstOrDefaultAsync<Category>();
        }

        public async Task<string> AddAsync(Category category)
        {
            category.Id = Guid.NewGuid().ToString();
            category.CreatedDate = DateTimeOffset.UtcNow.ToString("s");
            _dbContext.Categories.Add(category);
            return category.Id;
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
        }

        public async Task DeleteAsync(string id)
        {
            _dbContext.Remove(_dbContext.Categories.Single(i => i.Id == id));
        }
    }
}
