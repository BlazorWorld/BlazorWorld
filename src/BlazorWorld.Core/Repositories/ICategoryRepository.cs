using BlazorWorld.Core.Entities.Content;
using System.Threading.Tasks;

namespace BlazorWorld.Core.Repositories
{
    public interface ICategoryRepository : IRepository
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
