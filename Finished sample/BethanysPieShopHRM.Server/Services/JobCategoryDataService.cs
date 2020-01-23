using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;

        public JobCategoryDataService(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
            (await _httpClient.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            return await JsonSerializer.DeserializeAsync<JobCategory>
                (await _httpClient.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
