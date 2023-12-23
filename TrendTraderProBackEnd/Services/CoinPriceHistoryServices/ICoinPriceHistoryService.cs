using Entities.CoinPriceHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CoinPriceHistoryServices
{
    public interface ICoinPriceHistoryService
    {
        Task SetCoinPriceHistories(string coinIdStr);
        Task<List<CoinPriceHistoryDTO>> GetCoinPriceHistories(string coinIdStr, bool? isIncludePrice, bool? isIncludeMarketCap, bool? isIncludeTotalVolume, DateTime? minDate, DateTime? maxDate);
    }
}
