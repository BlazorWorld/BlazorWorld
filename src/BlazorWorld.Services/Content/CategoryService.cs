using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetAsync(string id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        public async Task<Category[]> GetAllAsync(string module)
        {
            return await _categoryRepository.GetAllAsync(module);
        }

        public async Task<Category> GetByNameAsync(string name, string module)
        {
            return await _categoryRepository.GetByNameAsync(name, module);
        }

        public async Task<Category> GetBySlugAsync(string slug, string module)
        {
            return await _categoryRepository.GetBySlugAsync(slug, module);
        }

        public async Task<string> AddAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category.Id;
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
