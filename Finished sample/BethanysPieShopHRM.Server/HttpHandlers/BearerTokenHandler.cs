using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace BethanysPieShopHRM.Server.HttpHandlers
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public BearerTokenHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            //var foo = httpClientFactory;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
            }

            return await base.SendAsync(request, cancellationToken);

        }

        public async Task<string> GetAccessTokenAsync()
        {
            string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            if (_httpClient.DefaultRequestHeaders==null) throw new NullReferenceException(nameof(_httpClient.DefaultRequestHeaders));
            

            const string BEARER = "Bearer";
            const string AUTHORIZATION = "Authorization";

            if (accessToken != null && _httpClient.DefaultRequestHeaders != null 
                                    && _httpClient.DefaultRequestHeaders.Any()
                                    && _httpClient.DefaultRequestHeaders.Authorization != null
                                    && _httpClient.DefaultRequestHeaders.Authorization.Scheme==BEARER) 
                
            {
                return accessToken;
            }

            _httpClient.DefaultRequestHeaders.Add(AUTHORIZATION, $"{BEARER} {accessToken}");

            return accessToken;

        }
    }
}
