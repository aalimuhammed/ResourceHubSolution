using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using System.Net.Http.Headers;

namespace ResourceHub.Infrastructure.Services
{
    public class SapSettings
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SapUrl { get; set; } = null!;
    }
    public class ResourceHubExternalService : IResourceHubExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<SapSettings> _settings;
        public ResourceHubExternalService(HttpClient httpClient ,IOptions<SapSettings>options)
        {
            _httpClient = httpClient;
            _settings = options;
            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{_settings.Value.UserName}:{_settings.Value.Password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
        public async Task<PaginatedResultDto<ServiceDto>> GetServicePageAsync(
            int pageNumber, 
            int pageSize,
            CancellationToken cancellationToken)
        {
            var offset = (pageNumber - 1) * pageSize;
            var top = pageSize;

            var url = _settings.Value.SapUrl
                     .Replace("{top}", pageSize.ToString())
                     .Replace("{skip}", offset.ToString());

            var httpResponse = await _httpClient.GetAsync(url , cancellationToken);

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var services = JsonConvert.DeserializeObject<IEnumerable<ServiceDto>>(content)!;

            var hasMore = httpResponse.Headers.TryGetValues("x-has-more", out var hasMoreValues)
                && hasMoreValues.FirstOrDefault()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true;

            var totalCount = 0;

            if (httpResponse.Headers.TryGetValues("x-row-count", out var rowCountValues))
            {
                int.TryParse(rowCountValues.FirstOrDefault(), out totalCount);
            }

            return new PaginatedResultDto<ServiceDto>
            {
                Items = services,
                HasMore = hasMore,
                TotalCount = totalCount
            };
        }
    }
}