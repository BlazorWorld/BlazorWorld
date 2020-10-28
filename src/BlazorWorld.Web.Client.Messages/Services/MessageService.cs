using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Client.Services;
using BlazorWorld.Web.Shared.Services;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Messages.Services
{
    public class MessageService : ApiService, IMessageService
    {
        private const string API_URL = "api/Message";
        private readonly IWebUserService _userService;

        public MessageService(
            IHttpClientFactory httpClientFactory,
            IWebUserService userService
            ) : base(httpClientFactory)
        {
            _userService = userService;
        }

        public async Task<Message[]> GetAsync(string groupId, int currentPage)
        {
            var request = $"{API_URL}?groupId={groupId}";
            var messages = await AuthorizedHttpClient.GetFromJsonAsync<Message[]>(request);
            messages.ToList().ForEach(async m => m.Username = await _userService.GetUserNameAsync(m.CreatedBy));
            return messages;
        }

        public async Task<int> GetCountAsync(string groupId)
        {
            var request = $"{API_URL}/getCount?groupId={groupId}";
            var response = await AuthorizedHttpClient.GetAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            int count;
            int.TryParse(responseString, out count);
            return count;
        }

        public async Task<int> GetPageSizeAsync(string module)
        {
            var request = $"{API_URL}/getPageSize?module={module}";
            var response = await AuthorizedHttpClient.GetAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            int size;
            int.TryParse(responseString, out size);
            return size;
        }
    }
}
