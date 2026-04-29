using System.Text.Json.Serialization;

namespace stocksTracker.Dtos.Finnhub
{
    public class FinnhubQuoteDto
    {
        [JsonPropertyName("c")]
        public decimal? CurrentPrice { get; set; } 

        [JsonPropertyName("d")]
        public decimal? Change { get; set; }     

        [JsonPropertyName("dp")]
        public decimal? ChangePercent { get; set; }  

        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }     

        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }       
    }
}
