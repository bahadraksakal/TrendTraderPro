using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CoinPriceHistories
{
    public class CoinPriceHistory
    {
        public int Id { get; set; }

        public string? CoinId { get; set; }

        [Column(TypeName = "decimal(18, 10)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MarketCap { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalVolume { get; set; }

        public DateTime Date { get; set; }
    }
}
