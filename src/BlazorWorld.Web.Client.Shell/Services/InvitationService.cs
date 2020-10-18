using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Web.Client.Common;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Services
{
    public class InvitationService : ApiService, IInvitationService
    {
        private const string API_URL = "api/Invitation";
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public InvitationService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<Invitation[]> GetAllAsync()
        {
            var request = $"{API_URL}/GetAll";
            return await AuthorizedHttpClient.GetFromJsonAsync<Invitation[]>(request);
        }

        public async Task<HttpResponseMessage> Invite(Invitation invitation)
        {
            return await AuthorizedHttpClient.PostAsJsonAsync<Invitation>(API_URL, invitation);
        }
    }
}
