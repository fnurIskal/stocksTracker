using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stocksTracker.Data;
using stocksTracker.Dtos.Stock;
using stocksTracker.Interfaces;
using stocksTracker.Mappers;

namespace stocksTracker.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IFinnhubService _finnhubService;

        // Dependency Injection: Dependencies are injected via constructor
        public StockController(IStockRepository stockRepo, IFinnhubService finnhubService)
        {
            _stockRepo = stockRepo;
            _finnhubService = finnhubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var exists = await _stockRepo.ExistsAsync(stockDto.Symbol);
            if (exists)
                return Conflict("This stock is already in your watchlist.");

            var quote = await _finnhubService.GetQuoteAsync(stockDto.Symbol.ToUpper());
            if (quote == null || quote.CurrentPrice == 0)
                return BadRequest("Stock not found. Please check the symbol and try again.");


            var stock = stockDto.ToStockFromCreateDto();
            stock.Symbol = stockDto.Symbol.ToUpper();
            stock.CurrentPrice = quote.CurrentPrice ?? 0;
            stock.CurrentPrice = quote.CurrentPrice ?? 0;
            stock.Change = quote.Change ?? 0;
            stock.ChangePercent = quote.ChangePercent ?? 0;
            stock.HighPrice = quote.HighPrice ?? 0;
            stock.LowPrice = quote.LowPrice ?? 0;

            await _stockRepo.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id}/refresh")]
        public async Task<IActionResult> Refresh([FromRoute] int id)
        {
            var stock = await _stockRepo.RefreshAsync(id);
            if (stock == null) return NotFound();

            var quote = await _finnhubService.GetQuoteAsync(stock.Symbol);
            if (quote == null) return BadRequest();

            stock.CurrentPrice = quote.CurrentPrice ?? 0;
            stock.Change = quote.Change ?? 0;
            stock.ChangePercent = quote.ChangePercent ?? 0;
            stock.HighPrice = quote.HighPrice ?? 0;
            stock.LowPrice = quote.LowPrice ?? 0;

            await _stockRepo.SaveChangesAsync();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock == null)
            {
            return NotFound(); 
            }
            return NoContent();
        }
        [HttpGet("analytics/top-gainers")]
        public async Task<IActionResult> GetTopGainers([FromQuery] int count = 5)
        {
            var stocks = await _stockRepo.GetTopGainersAsync(count);
            return Ok(stocks.Select(s => s.ToStockDto()));
        }

        [HttpGet("analytics/summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _stockRepo.GetSummaryAsync();
            return Ok(summary);
        }

    }
}
