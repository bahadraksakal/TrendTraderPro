using Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GeckoApiServices
{
    public interface ICoinService
    {
        Task<List<CoinDTO>> SetCoins();
        Task<CoinDTO> GetCoinIdByNameAsync(string coinName);
    }
}
