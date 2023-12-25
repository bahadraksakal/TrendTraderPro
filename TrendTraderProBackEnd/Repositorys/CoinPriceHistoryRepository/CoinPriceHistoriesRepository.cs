using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.CoinPriceHistories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositorys.CoinPriceHistoryRepository
{
    public class CoinPriceHistoriesRepository : ICoinPriceHistoriesRepository
    {
        private readonly TrendTraderProDbContext _trendTraderProDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICoinPriceHistoriesRepository> _logger;

        public CoinPriceHistoriesRepository(TrendTraderProDbContext trendTraderProDbContext, IMapper mapper, ILogger<ICoinPriceHistoriesRepository> logger)
        {
            _trendTraderProDbContext = trendTraderProDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SetCoinPriceHistorys(List<CoinPriceHistoryDTO> coinPriceHistoriesDTO)
        {
            List<CoinPriceHistory> coinPriceHistories = _mapper.Map<List<CoinPriceHistory>>(coinPriceHistoriesDTO);

            await _trendTraderProDbContext.CoinPriceHistories.AddRangeAsync(coinPriceHistories);
            await _trendTraderProDbContext.SaveChangesAsync();

            _logger.LogInformation($"CoinPriceHistorys is Successed. CoinId - {coinPriceHistories[0]?.CoinId}: [Added CoinPriceHistorys Count:{coinPriceHistories?.Count}]");
        }

        public async Task<List<CoinPriceHistoryDTO>> GetCoinPricesHistories(string coinIdStr, bool? isIncludePrice = false, bool? isIncludeMarketCap =false, bool? isIncludeTotalVolume =false, DateTime? minDate = null, DateTime? maxDate = null)
        {
            IQueryable<CoinPriceHistory> query = _trendTraderProDbContext.CoinPriceHistories
                .AsNoTracking()
                .Select(c => new CoinPriceHistory
                {
                    Id = c.Id,
                    CoinId = c.CoinId,
                    Price = isIncludePrice.GetValueOrDefault() ? c.Price : null,
                    MarketCap = isIncludeMarketCap.GetValueOrDefault() ? c.MarketCap : null,
                    TotalVolume = isIncludeTotalVolume.GetValueOrDefault() ? c.TotalVolume : null,
                    Date = c.Date
                })
                .Where(coinPriceHistory => coinPriceHistory.CoinId == coinIdStr)
                .OrderByDescending(coinPriceHistory => coinPriceHistory.Date);

            if (minDate.HasValue)
            {
                query = query.Where(coinPriceHistory => coinPriceHistory.Date >= minDate);
            }

            if (maxDate.HasValue)
            {
                query = query.Where(coinPriceHistory => coinPriceHistory.Date <= maxDate.Value);
            }

            List<CoinPriceHistory> coinPriceHistories = await query.ToListAsync();
            
            return _mapper.Map<List<CoinPriceHistoryDTO>>(coinPriceHistories);

        }

        public async Task<CoinPriceHistoryDTO> GetCoinPricesHistoryLastData(string coinIdStr)
        {
            CoinPriceHistory? coinPriceLastData = await _trendTraderProDbContext.CoinPriceHistories
                .AsNoTracking()
                .Where(coinPriceHistory => coinPriceHistory.CoinId == coinIdStr)
                .OrderByDescending(coinPriceHistory => coinPriceHistory.Date)
                .FirstOrDefaultAsync();

            return _mapper.Map<CoinPriceHistoryDTO>(coinPriceLastData);
        }



    }
}
