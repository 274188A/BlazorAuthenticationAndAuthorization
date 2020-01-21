using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BethanysPieShopHRM.Server.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CountryDataService(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ??
                          throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ??
                                   throw new System.ArgumentNullException(nameof(httpContextAccessor));

        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>
            (await _httpClient.GetStreamAsync($"api/country"), new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true });


        }

        public async Task<Country> GetCountryById(int countryId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            return await JsonSerializer.DeserializeAsync<Country>
                (await _httpClient.GetStreamAsync($"api/country{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
