using stocksTracker.Dtos.Finnhub;

namespace stocksTracker.Interfaces
{
    public interface IFinnhubService
    {
        // Service Pattern: Dış API ile iletişimi controller'dan ayırıyoruz
        // Controller sadece iş mantığıyla ilgilenir, HTTP isteklerini servis yönetir

            Task<FinnhubQuoteDto?> GetQuoteAsync(string symbol);
    }
}
