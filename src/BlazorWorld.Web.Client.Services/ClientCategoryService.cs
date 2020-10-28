using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientCategoryService : ApiService, IWebCategoryService
    {
        private const string API_URL = "api/Category";
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ClientCategoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<Category> GetAsync(string id)
        {
            var request = $"{API_URL}?id={id}";
            return await PublicHttpClient.GetFromJsonAsync<Category>(request);
        }

        public async Task<Category[]> GetAllAsync(string module)
        {
            var request = $"{API_URL}/GetAll?module={module}";
            return await PublicHttpClient.GetFromJsonAsync<Category[]>(request);
        }

        public async Task<Category> GetByNameAsync(string name, string module)
        {
            var request = $"{API_URL}/GetByName?name={name}&module={module}";
            return await AuthorizedHttpClient.GetFromJsonAsync<Category>(request);
        }

        public async Task<Category> GetBySlugAsync(string slug, string module)
        {
            var request = $"{API_URL}/GetBySlug?slug={slug}&module={module}";
            return await PublicHttpClient.GetFromJsonAsync<Category>(request);
        }

        public async Task<HttpResponseMessage> AddAsync(Category category)
        {
            var response = await AuthorizedHttpClient.PostAsJsonAsync<Category>(API_URL, category);
            var id = await response.Content.ReadAsStringAsync();
            category.Id = id;

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync(Category category)
        {
            return await AuthorizedHttpClient.PutAsJsonAsync<Category>(API_URL, category);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await AuthorizedHttpClient.DeleteAsync($"{API_URL}/?id={id}");
        }
    }
}
