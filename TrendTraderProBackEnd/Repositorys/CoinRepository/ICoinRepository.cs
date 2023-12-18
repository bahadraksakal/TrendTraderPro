using Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.CoinRepository
{
    public interface ICoinRepository
    {
        Task<CoinDTO> AddCoinAsync(string idStr, string coinName, string coinSymbol);
        Task<CoinDTO> GetCoinByNameAsync(string coinName);
        Task<List<CoinDTO>> GetAllCoinsAsync();
        Task<List<CoinDTO>> SetCoinsAsync(List<Coin> newApiCoins);
    }
}
