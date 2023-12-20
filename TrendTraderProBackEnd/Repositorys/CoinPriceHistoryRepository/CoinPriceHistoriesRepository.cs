using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.CoinPriceHistories;
using Entities.CoinPriceHistorys;
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

        public async Task SetCoinPriceHistorys(List<CoinPriceHistory> coinPriceHistories)
        {
            //kontrol işlemi ekle son tarihten itibaren eklesin gerçi bu servie içinde olcak :)
            _trendTraderProDbContext.CoinPriceHistories.AddRange(coinPriceHistories);
            await _trendTraderProDbContext.SaveChangesAsync();
            _logger.LogInformation($"CoinPriceHistorys is Successed. CoinId - {coinPriceHistories[0]?.CoinId}: [Added CoinPriceHistorys Count:{coinPriceHistories?.Count}]");
        }

        public async Task<List<CoinPriceHistoryDTO>> GetCoinPricesHistories(string coinIdStr, bool isIncludePrice, bool isIncludeMarketCap, bool isIncludeTotalVolume, DateTime? minDate = null, DateTime? maxDate = null)
        {
            IQueryable<CoinPriceHistory> query = _trendTraderProDbContext.CoinPriceHistories
                .AsNoTracking()
                .Where(coinPriceHistory => coinPriceHistory.CoinId == coinIdStr);

            if (minDate.HasValue)
            {
                query = query.Where(coinPriceHistory => coinPriceHistory.Date >= minDate);
            }

            if (maxDate.HasValue)
            {
                query = query.Where(coinPriceHistory => coinPriceHistory.Date <= maxDate.Value);
            }

            if (isIncludePrice)
            {
                query = query.Include(c => c.Price);
            }

            if (isIncludeMarketCap)
            {
                query = query.Include(c => c.MarketCap);
            }

            if (isIncludeTotalVolume)
            {
                query = query.Include(c => c.TotalVolume);
            }

            List<CoinPriceHistory> coinPriceHistories = await query.ToListAsync();
            return _mapper.Map<List<CoinPriceHistoryDTO>>(coinPriceHistories);
        }


    }
}
