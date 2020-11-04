using BlazorWorld.Web.Shared.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    public class ClientUserService : ApiService, IWebUserService
    { 
        private const string API_URL = "api/User";
        //private readonly ProtectedSessionStorage _sessionStorageService;
        private readonly ILogger<ClientUserService> _logger;

        public ClientUserService(
            IHttpClientFactory httpClientFactory,
            //ProtectedSessionStorage sessionStorageService,
            ILogger<ClientUserService> logger) : base(httpClientFactory)
        {
            //_sessionStorageService = sessionStorageService;
            _logger = logger;
        }

        public async Task<string> GetUserNameAsync(string appUserId)
        {
            var key = $"{appUserId}-username";
            //var username = await _sessionStorageService.GetItemAsync<string>(key);
            var username = string.Empty;
            if (string.IsNullOrEmpty(username))
            {
                var request = $"{API_URL}/GetUserName?appUserId={appUserId}";
                username = await PublicHttpClient.GetStringAsync(request);
                //await _sessionStorageService.SetItemAsync<string>(key, username);
            }
            return username;
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var key = $"{username}-userId";
            //var appUserId = await _sessionStorageService.GetItemAsync<string>(key);
            var appUserId = string.Empty;
            if (string.IsNullOrEmpty(appUserId))
            {
                var request = $"{API_URL}/GetUserId?username={username}";
                appUserId = await PublicHttpClient.GetStringAsync(request);
                //await _sessionStorageService.SetItemAsync<string>(key, appUserId);
            }
            return appUserId;
        }

        public async Task<string> GetAvatarHashAsync(string appUserId)
        {
            var key = $"{appUserId}-avatarhash";
            //var avatarhash = await _sessionStorageService.GetItemAsync<string>(key);
            var avatarhash = string.Empty;
            if (string.IsNullOrEmpty(avatarhash))
            {
                var request = $"{API_URL}/GetAvatarHash?appUserId={appUserId}";
                avatarhash = await PublicHttpClient.GetStringAsync(request);
                //await _sessionStorageService.SetItemAsync<string>(key, avatarhash);
            }
            return avatarhash;
        }
    }
}
