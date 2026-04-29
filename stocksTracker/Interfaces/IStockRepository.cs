using stocksTracker.Dtos.Stock;
using stocksTracker.Models;

namespace stocksTracker.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock> RefreshAsync(int id);
        Task<Stock> DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<List<Stock>> GetTopGainersAsync(int count);
        Task<StockSummaryDto> GetSummaryAsync();
    }
}
