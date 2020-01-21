using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BethanysPieShopHRM.Server.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobCategoryDataService(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ??
                          throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ??
                                   throw new System.ArgumentNullException(nameof(httpContextAccessor));

        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {

            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }

            return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
            (await _httpClient.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }


            return await JsonSerializer.DeserializeAsync<JobCategory>
                (await _httpClient.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
