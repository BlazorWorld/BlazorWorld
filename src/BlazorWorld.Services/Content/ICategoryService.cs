using BlazorWorld.Core.Entities.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(string id);
        Task<Category[]> GetAllAsync(string module);
        Task<Category> GetByNameAsync(string name, string module);
        Task<Category> GetBySlugAsync(string slug, string module);
        Task<string> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);
    }
}
