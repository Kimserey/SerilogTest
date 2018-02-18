using IdentityModel.Client;
using Serilog.Sinks.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared
{
    public class OidcHttpClient : IHttpClient
    {
        private readonly string _client;
        private readonly string _clientSecret;
        private readonly string _authorityUri;
        private readonly HttpClient _http;

        public OidcHttpClient(string client, string clientSecret, string authorityUri)
        {
            _http = new HttpClient();
            _client = client;
            _clientSecret = clientSecret;
            _authorityUri = authorityUri;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            var success = await SetBearer();
            if (!success)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden
                };
            }

            try
            {
                await _http.PostAsync(requestUri, content);

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public void Dispose()
        {
            _http.Dispose();
        }

        private async Task<bool> SetBearer()
        {
            var disco = await DiscoveryClient.GetAsync(_authorityUri);
            if (disco.IsError)
            {
                return false;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, _client, _clientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("log-api");

            if (tokenResponse.IsError)
            {
                return false;
            }

            _http.SetBearerToken(tokenResponse.AccessToken);
            return true;
        }
    }
}
