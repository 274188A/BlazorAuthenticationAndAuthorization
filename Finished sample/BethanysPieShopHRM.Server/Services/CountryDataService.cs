using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;


        public CountryDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>
            (await _httpClient.GetStreamAsync($"api/country"), new JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true });
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await JsonSerializer.DeserializeAsync<Country>
                (await _httpClient.GetStreamAsync($"api/country{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
