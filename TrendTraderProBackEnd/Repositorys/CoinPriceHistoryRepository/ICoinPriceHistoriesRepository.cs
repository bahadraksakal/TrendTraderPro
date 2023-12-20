using Entities.CoinPriceHistories;
using Entities.CoinPriceHistorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.CoinPriceHistoryRepository
{
    public interface ICoinPriceHistoriesRepository
    {

        Task SetCoinPriceHistorys(List<CoinPriceHistory> coinPriceHistories);
        Task<List<CoinPriceHistoryDTO>> GetCoinPricesHistories(string coinIdStr, bool isIncludePrice, bool isIncludeMarketCap, bool isIncludeTotalVolume, DateTime? minDate = null, DateTime? maxDate = null);
    }
}
