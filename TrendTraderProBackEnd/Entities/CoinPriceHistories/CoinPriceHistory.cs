using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CoinPriceHistorys
{
    public class CoinPriceHistory
    {
        public int Id { get; set; }

        public string? CoinId { get; set; }

        [Column(TypeName = "decimal(18, 10)")]
        public float? Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MarketCap { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalVolume { get; set; }

        public DateTime Date { get; set; }
    }
}
