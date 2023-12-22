using Entities.CoinPriceHistories;

namespace Repositorys.CoinPriceHistoryRepository
{
    public interface ICoinPriceHistoriesRepository
    {

        Task SetCoinPriceHistorys(List<CoinPriceHistoryDTO> coinPriceHistoriesDTO);
        Task<List<CoinPriceHistoryDTO>> GetCoinPricesHistories(string coinIdStr, bool isIncludePrice, bool isIncludeMarketCap, bool isIncludeTotalVolume, DateTime? minDate = null, DateTime? maxDate = null);
        Task<CoinPriceHistoryDTO> GetCoinPricesHistoryLastData(string coinIdStr);
    }
}
