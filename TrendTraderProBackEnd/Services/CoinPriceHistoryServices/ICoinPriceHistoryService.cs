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
    }
}
