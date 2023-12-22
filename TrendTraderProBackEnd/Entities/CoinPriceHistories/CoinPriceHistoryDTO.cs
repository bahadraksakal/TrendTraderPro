namespace Entities.CoinPriceHistories
{
    public class CoinPriceHistoryDTO
    {
        public int Id { get; set; }
        public string? CoinId { get; set; }
        public decimal? Price { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? TotalVolume { get; set; }
        public DateTime? Date { get; set; }
    }
}
