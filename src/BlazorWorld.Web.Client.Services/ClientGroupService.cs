using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Web.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientGroupService : ApiService, IWebGroupService
    {
        private const string API_URL = "api/Group";
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ClientGroupService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<Group> SecureGetAsync(string id)
        {
            var request = $"{API_URL}?id={id}";
            return await AuthorizedHttpClient.GetFromJsonAsync<Group>(request);
        }

        public async Task<Group[]> SecureGetAllAsync(string module)
        {
            var request = $"{API_URL}/GetAll?module={module}";
            return await AuthorizedHttpClient.GetFromJsonAsync<Group[]>(request);
        }

        public async Task<Group> SecureGetByKeyAsync(string key)
        {
            var request = $"{API_URL}/GetByKey?key={key}";
            return await AuthorizedHttpClient.GetFromJsonAsync<Group>(request);
        }

        public async Task<GroupMember[]> SecureGetGroupMembersAsync(string groupId)
        {
            var request = $"{API_URL}/GetMembers?groupId={groupId}";
            return await AuthorizedHttpClient.GetFromJsonAsync<GroupMember[]>(request);
        }

        public async Task<HttpResponseMessage> PostAsync(Group group)
        {
            var response = await AuthorizedHttpClient.PostAsJsonAsync<Group>(API_URL, group);
            var id = await response.Content.ReadAsStringAsync();
            group.Id = id;

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(Group group)
        {
            return await AuthorizedHttpClient.PutAsJsonAsync<Group>(API_URL, group);
        }
    }
}
