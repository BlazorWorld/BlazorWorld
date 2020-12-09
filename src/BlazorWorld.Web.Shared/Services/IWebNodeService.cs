using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Web.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Shared.Services
{
    public interface IWebNodeService
    {
        Task<Node> GetAsync(string id);
        Task<Node> GetBySlugAsync(string module, string type, string slug,
            bool noStore = false);
        Task<Node[]> GetAsync(NodeSearch nodeSearch, int currentPage);
        Task<int> GetCountAsync(NodeSearch nodeSearch);
        Task<int> GetPageSizeAsync(NodeSearch nodeSearch);
        Task<Node> SecureGetAsync(string id);
        Task<Node> SecureGetAsync(string module, string type, string slug);
        Task<Node[]> SecureGetAsync(
            NodeSearch nodeSearch,
            int currentPage);
        Task<int> SecureGetCountAsync(NodeSearch nodeSearch);
        Task<HttpResponseMessage> AddAsync(ContentActivity contentActivity);
        Task<HttpResponseMessage> UpdateAsync(ContentActivity contentActivity);
        Task<HttpResponseMessage> DeleteAsync(string id);
        Task<string> SecureGetOEmbed(string oEmbedUrl);
    }
}
