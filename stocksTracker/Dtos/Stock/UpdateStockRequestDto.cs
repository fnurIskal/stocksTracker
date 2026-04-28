namespace stocksTracker.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
    }
}
