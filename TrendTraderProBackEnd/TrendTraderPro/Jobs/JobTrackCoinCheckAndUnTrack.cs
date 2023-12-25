using Entities.TrackCoins;
using Services.TrackCoinServices;

namespace TrendTraderPro.Jobs
{
    public class JobTrackCoinCheckAndUnTrack
    {
        private readonly ITrackCoinService _trackCoinService;
        private readonly ILogger<JobTrackCoinCheckAndUnTrack> _logger;

        public JobTrackCoinCheckAndUnTrack(ITrackCoinService trackCoinService, ILogger<JobTrackCoinCheckAndUnTrack> logger)
        {
            _trackCoinService = trackCoinService;
            _logger = logger;
        }

        public async Task TrackCoinCheckAndUnTrackExecute(long second)
        {
            _logger.LogInformation("JobTrackCoinCheckAndUnTrack started.");
            List<TrackCoinDTO> trackCoinDTOs = await _trackCoinService.GetAllTrackCoinsOnlyTrackedAsync();
            foreach (TrackCoinDTO trackCoinDTO in trackCoinDTOs)
            {
                await _trackCoinService.SetUnTrackCoinToLastRequestAsync(trackCoinDTO.CoinId ?? "", second);
            }
            _logger.LogInformation("JobTrackCoinCheckAndUnTrack finished.");
        }
    }
}
