using Entities.CoinPriceHistories;
using Entities.TrackCoins;
using System.ComponentModel.DataAnnotations;

namespace Entities.Coins
{
    public class Coin
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string? Symbol { get; set; }

        [Required]
        public string? Name { get; set; }

        public virtual TrackCoin? TrackCoin { get; set; }
        public virtual ICollection<CoinPriceHistory>? CoinPriceHistory { get; set; }
    }
}
