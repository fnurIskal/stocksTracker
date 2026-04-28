using Microsoft.Extensions.Options;
using stocksTracker.Dtos.Finnhub;
using stocksTracker.Models;
using System.Text.Json;
using stocksTracker.Interfaces;

namespace stocksTracker.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly FinnhubSettings _settings;

        public FinnhubService(HttpClient httpClient, IOptions<FinnhubSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<FinnhubQuoteDto?> GetQuoteAsync(string symbol)
        {
            // Finnhub quote endpoint'i
            var url = $"{_settings.BaseUrl}/quote?symbol={symbol}&token={_settings.ApiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var quote = JsonSerializer.Deserialize<FinnhubQuoteDto>(content);

            return quote;
        }
    }
}