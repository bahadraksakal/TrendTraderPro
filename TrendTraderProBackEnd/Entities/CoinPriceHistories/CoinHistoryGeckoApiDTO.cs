using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CoinPriceHistorys
{
    public class CoinHistoryGeckoApiDTO
    {
        public List<List<decimal>>? prices { get; set; }
        public List<List<decimal>>? market_caps { get; set; }
        public List<List<decimal>>? total_volumes { get; set; }
    }
}
