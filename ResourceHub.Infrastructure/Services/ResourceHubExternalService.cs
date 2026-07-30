using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ResourceHub.Infrastructure.Services
{
    public class SapSettings
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SapUrl { get; set; } = null!;

    }

    // ResourceHubExternalService as sap client
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
        async Task<ServicePageResult> IResourceHubExternalService.GetServicePageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var skip = (pageNumber - 1) * pageSize;
            var top = pageSize;

            var url = _settings.Value.SapUrl.ToString()!
                .Replace("{skip}", skip.ToString())
                .Replace("{top}", top.ToString());

            var httpResponse = await _httpClient.GetAsync(url);

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var services = JsonConvert.DeserializeObject<ICollection<ServiceDto>>(content)!;

            var hasMore = httpResponse.Headers.TryGetValues("x-has-more", out var hasMoreValues)
                && hasMoreValues.FirstOrDefault()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true;

            return new ServicePageResult
            {
                Services = services,
                HasMore = hasMore
            };
        }
    }
}
