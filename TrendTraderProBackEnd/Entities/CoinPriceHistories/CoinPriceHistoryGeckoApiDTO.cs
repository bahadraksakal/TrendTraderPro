using System.Text.Json.Serialization;

namespace Entities.CoinPriceHistories
{
    public class CoinPriceHistoryGeckoApiDTO
    {
        public List<decimal[]>? prices { get; set; }

        public List<decimal[]>? market_caps { get; set; }

        public List<decimal[]>? total_volumes { get; set; }
    }
}
