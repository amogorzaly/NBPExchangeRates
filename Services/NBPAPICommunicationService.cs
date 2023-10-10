using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NBPExchangeRates.Interfaces;
using NBPExchangeRates.Models;
using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Services
{
    public class NBPAPICommunicationService : INBPAPICommunicationService
    {
        private readonly IOptions<NbpApiOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        public NBPAPICommunicationService(IOptions<NbpApiOptions> options, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }
        public async Task<List<NBPAPIResponseViewModel>> GetLatestExchangeRatesFromApi()
        {
            try
            {
                if (_memoryCache.TryGetValue(APIExchangeRateCacheKey, out List<NBPAPIResponseViewModel?> cachedValue) && cachedValue is not null)
                {
                    return cachedValue;
                }
                var request = new HttpRequestMessage(HttpMethod.Get, _options.Value.ApiEndpoint);
                request.Headers.Add("Accept", "application/json");
                var httpClient = _httpClientFactory.CreateClient("NBPAPIClient");
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<NBPAPIResponseViewModel>>(content) ?? new List<NBPAPIResponseViewModel>();
                _memoryCache.Set(APIExchangeRateCacheKey, result, TimeSpan.FromMinutes(10));
                return result;
            }
            catch
            {
                return new List<NBPAPIResponseViewModel>();
            }
        }
        private static readonly string APIExchangeRateCacheKey = "latestExchangeRateFromAPI";

    }
}
