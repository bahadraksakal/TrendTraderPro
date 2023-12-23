using Entities.Coins;
using System.ComponentModel.DataAnnotations;

namespace Entities.TrackCoins
{
    public class TrackCoin
    {
        [Key]
        public string? CoinId { get; set; }

        public TrackStatus? TrackStatus { get; set; }

        public DateTime? LastRequestDate { get; set; }



        public virtual Coin? Coin { get; set; }
    }
}
