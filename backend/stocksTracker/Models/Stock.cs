using Microsoft.EntityFrameworkCore;

namespace stocksTracker.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        [Precision(18, 2)]
        public decimal CurrentPrice { get; set; }

        [Precision(18, 2)]
        public decimal Change { get; set; }

        [Precision(18, 2)]
        public decimal ChangePercent { get; set; }

        [Precision(18, 2)]
        public decimal HighPrice { get; set; }

        [Precision(18, 2)]
        public decimal LowPrice { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
