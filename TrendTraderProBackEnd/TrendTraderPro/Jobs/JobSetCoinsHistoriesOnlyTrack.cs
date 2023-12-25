using Entities.TrackCoins;
using Services.CoinPriceHistoryServices;
using Services.TrackCoinServices;

namespace TrendTraderPro.Jobs
{
    public class JobSetCoinsHistoriesOnlyTrack
    {
        private readonly ICoinPriceHistoryService _coinHistoryService;
        private readonly ITrackCoinService _trackCoinService;
        private readonly ILogger<JobSetCoinsHistoriesOnlyTrack> _logger;
    
        public JobSetCoinsHistoriesOnlyTrack(ICoinPriceHistoryService coinHistoryService, ITrackCoinService trackCoinService, ILogger<JobSetCoinsHistoriesOnlyTrack> logger)
        {
            _coinHistoryService = coinHistoryService;
            _trackCoinService = trackCoinService;
            _logger = logger;
        }

        public async Task SetCoinsHistoriesOnlyTrackExecute()
        {
            _logger.LogInformation("JobSetCoinsHistoriesOnlyTrack started.");
            List<TrackCoinDTO> trackCoinDTOs = await _trackCoinService.GetAllTrackCoinsOnlyTrackedAsync();
            foreach (TrackCoinDTO trackCoinDTO in trackCoinDTOs)
            {
                await _coinHistoryService.SetCoinPriceHistoriesAsync(trackCoinDTO.CoinId ?? "");
            }
            _logger.LogInformation("JobSetCoinsHistoriesOnlyTrack finished.");
        }
    }
}
