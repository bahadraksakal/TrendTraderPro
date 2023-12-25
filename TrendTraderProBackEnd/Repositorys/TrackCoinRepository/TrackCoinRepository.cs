using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.TrackCoins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositorys.TrackCoinRepository
{
    public class TrackCoinRepository : ITrackCoinRepository
    {
        private readonly TrendTraderProDbContext _trendTraderProDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ITrackCoinRepository> _logger;

        public TrackCoinRepository(TrendTraderProDbContext trendTraderProDbContext, IMapper mapper, ILogger<ITrackCoinRepository> logger)
        {
            _trendTraderProDbContext = trendTraderProDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TrackCoinDTO> AddTrackCoinAsync(string coinIdStr, DateTime lastRequestDate, TrackStatus trackStatus)
        {
            TrackCoin trackCoin =new TrackCoin
            {
                CoinId = coinIdStr,
                TrackStatus = trackStatus,
                LastRequestDate = lastRequestDate
            };
            await _trendTraderProDbContext.AddAsync(trackCoin);
            await _trendTraderProDbContext.SaveChangesAsync();
            _logger.LogInformation($"TrackCoin Başarıyla Eklendi: [CoinId:{trackCoin.CoinId} - TrackStatus:{trackCoin.TrackStatus} - LastRequestDate:{trackCoin.LastRequestDate}]");
            return _mapper.Map<TrackCoinDTO>(trackCoin);
        }

        public async Task<TrackCoinDTO> GetTrackCoinAsync(string coinIdStr)
        {
            TrackCoin? trackCoin = await _trendTraderProDbContext.TrackCoins.AsNoTracking().FirstOrDefaultAsync(TrackCoin => TrackCoin.CoinId == coinIdStr);
            return _mapper.Map<TrackCoinDTO>(trackCoin);
        }

        public async Task<List<TrackCoinDTO>> GetAllTrackCoinsAsync()
        {
            List<TrackCoin>? trackCoins = await _trendTraderProDbContext.TrackCoins.AsNoTracking().ToListAsync();
            return _mapper.Map<List<TrackCoinDTO>>(trackCoins);
        }

        public async Task<TrackCoinDTO> UpdateTrackCoin(string coinIdStr, DateTime newLastRequestDate, TrackStatus? trackStatus = TrackStatus.Tracked)
        {
            TrackCoin? trackCoin = await _trendTraderProDbContext.TrackCoins.FirstOrDefaultAsync(trackCoin => trackCoin.CoinId == coinIdStr);
            
            if(trackCoin != null)
            {
                trackCoin.TrackStatus = trackStatus;
                trackCoin.LastRequestDate = newLastRequestDate;
                await _trendTraderProDbContext.SaveChangesAsync();
            } 
            return _mapper.Map<TrackCoinDTO>(trackCoin);
        }
       
    }
}
