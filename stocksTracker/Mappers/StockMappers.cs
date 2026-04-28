using stocksTracker.Dtos.Stock;
using stocksTracker.Models;

namespace stocksTracker.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                CurrentPrice = stockModel.CurrentPrice,
                Change = stockModel.Change,
                ChangePercent = stockModel.ChangePercent,
                HighPrice = stockModel.HighPrice,
                LowPrice = stockModel.LowPrice,
                LastUpdated = stockModel.LastUpdated,

            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto dto )
        {
            return new Stock
            {
                Symbol = dto.Symbol,
                CompanyName = dto.CompanyName,
                LastUpdated = DateTime.Now
            };
        }
    }
}
