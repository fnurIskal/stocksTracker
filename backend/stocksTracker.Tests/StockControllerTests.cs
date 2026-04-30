using Microsoft.AspNetCore.Mvc;
using Moq;
using stocksTracker.Controllers;
using stocksTracker.Dtos.Finnhub;
using stocksTracker.Dtos.Stock;
using stocksTracker.Interfaces;
using stocksTracker.Models;

namespace stocksTracker.Tests
{
    public class StockControllerTests
    {
        private readonly Mock<IStockRepository> _mockRepo;
        private readonly Mock<IFinnhubService> _mockFinnhub;
        private readonly StockController _controller;

        public StockControllerTests()
        {
            _mockRepo = new Mock<IStockRepository>();
            _mockFinnhub = new Mock<IFinnhubService>();
            _controller = new StockController(_mockRepo.Object, _mockFinnhub.Object);
        }

        [Fact]
        public async Task Create_ReturnsConflict_WhenStockAlreadyExists()
        {

            var dto = new CreateStockRequestDto { Symbol = "AAPL" };
            _mockRepo.Setup(r => r.ExistsAsync("AAPL")).ReturnsAsync(true);

            var result = await _controller.Create(dto);
    
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenFinnhubReturnsNull()
        {
          
            var dto = new CreateStockRequestDto { Symbol = "INVALID" };
            _mockRepo.Setup(r => r.ExistsAsync("INVALID")).ReturnsAsync(false);
            _mockFinnhub.Setup(f => f.GetQuoteAsync("INVALID")).ReturnsAsync((FinnhubQuoteDto?)null);

            var result = await _controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}