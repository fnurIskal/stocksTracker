namespace stocksTracker.Dtos.Stock
{
    public class StockSummaryDto
    {
        public int TotalStocks { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal TotalValue { get; set; }
        public string? BestPerformer { get; set; }
        public string? WorstPerformer { get; set; }
    }
}
