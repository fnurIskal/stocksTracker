using Microsoft.EntityFrameworkCore;
using stocksTracker.Data;
using stocksTracker.Dtos.Stock;
using stocksTracker.Interfaces;
using stocksTracker.Models;

namespace stocksTracker.Repository
{
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

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.CurrentPrice = stockDto.CurrentPrice;
            existingStock.Change = stockDto.Change;
            existingStock.ChangePercent = stockDto.ChangePercent;
            existingStock.HighPrice = stockDto.HighPrice;
            existingStock.LowPrice = stockDto.LowPrice;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}
