using BlazorWorld.Core.Entities.Content;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Common.Services
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(string id);
        Task<Category[]> GetAllAsync(string module);
        Task<Category> GetByNameAsync(string name, string module);
        Task<Category> GetBySlugAsync(string slug, string module);
        Task<HttpResponseMessage> AddAsync(Category category);
        Task<HttpResponseMessage> UpdateAsync(Category category);
        Task<HttpResponseMessage> DeleteAsync(string id);
    }
}
