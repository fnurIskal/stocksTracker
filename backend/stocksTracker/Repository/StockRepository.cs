using Microsoft.EntityFrameworkCore;
using stocksTracker.Data;
using stocksTracker.Dtos.Stock;
using stocksTracker.Interfaces;
using stocksTracker.Models;

namespace stocksTracker.Repository
{
    // Repository Pattern: Abstracts data access logic from business logic
    // Controller communicates with DB only through this interface
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();

        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> RefreshAsync(int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null) return null;
            return stock;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetTopGainersAsync(int count)
        {
            return await _context.Stock
                .OrderByDescending(s => s.ChangePercent)
                .Take(count)
                .ToListAsync();
        }

        public async Task<StockSummaryDto> GetSummaryAsync()
        {
            var stocks = await _context.Stock.ToListAsync();

            if (!stocks.Any())
                return new StockSummaryDto();

            return new StockSummaryDto
            {
                TotalStocks = stocks.Count,
                AveragePrice = stocks.Average(s => s.CurrentPrice),
                TotalValue = stocks.Sum(s => s.CurrentPrice),
                BestPerformer = stocks.OrderByDescending(s => s.ChangePercent).First().Symbol,
                WorstPerformer = stocks.OrderBy(s => s.ChangePercent).First().Symbol
            };
        }

        public async Task<bool> ExistsAsync(string symbol)
        {
            return await _context.Stock.AnyAsync(s => s.Symbol.ToUpper() == symbol.ToUpper());
        }


    }
}
