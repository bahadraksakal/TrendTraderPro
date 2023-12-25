using AutoMapper;
using Entities.TrackCoins;
using Microsoft.Extensions.Logging;
using Repositorys.TrackCoinRepository;
using System.Diagnostics.Contracts;

namespace Services.TrackCoinServices
{
    public class TrackCoinService : ITrackCoinService
    {
        private readonly ITrackCoinRepository _trackCoinRepository;
        private readonly IMapper _mapper;   
        private readonly ILogger<ITrackCoinService> _logger;

        public TrackCoinService(ITrackCoinRepository trackCoinRepository, IMapper mapper, ILogger<ITrackCoinService> logger)
        {
            _trackCoinRepository = trackCoinRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TrackCoinDTO> SetTrackCoinAsync(string coinIdStr)
        {
            TrackCoinDTO trackCoinDTO = await _trackCoinRepository.GetTrackCoinAsync(coinIdStr);
            if (trackCoinDTO is null)
            {
                trackCoinDTO = await _trackCoinRepository.AddTrackCoinAsync(coinIdStr, DateTime.Now, TrackStatus.Tracked);
            }
            else
            {
                trackCoinDTO = await _trackCoinRepository.UpdateTrackCoin(coinIdStr, DateTime.Now, TrackStatus. Tracked);
            }
            
            return trackCoinDTO;
        }

        public async Task<TrackCoinDTO> SetUnTrackCoinToLastRequestAsync(string coinIdStr, long second)
        {
            TrackCoinDTO trackCoinDTO = await _trackCoinRepository.GetTrackCoinAsync(coinIdStr);
            
            if(trackCoinDTO.LastRequestDate?.AddSeconds(second) < DateTime.Now)
            {
                trackCoinDTO = await _trackCoinRepository.UpdateTrackCoin(coinIdStr, trackCoinDTO.LastRequestDate.Value, TrackStatus.UnTracked);
                _logger.LogInformation($"TrackCoin was uptaded succesful: [CoinId:{trackCoinDTO.CoinId} - TrackStatus:{trackCoinDTO.TrackStatus} - LastRequestDate:{trackCoinDTO.LastRequestDate}]");
            }
            return trackCoinDTO;
        }

        public async Task<List<TrackCoinDTO>> GetAllTrackCoinsAsync()
        {
            List<TrackCoinDTO>? trackCoinDTOs = await _trackCoinRepository.GetAllTrackCoinsAsync();
            return trackCoinDTOs;
        }

        public async Task<List<TrackCoinDTO>> GetAllTrackCoinsOnlyTrackedAsync()
        {
            List<TrackCoinDTO>? trackCoinDTOs = await _trackCoinRepository.GetAllTrackCoinsOnlyTrackedAsync();
            return trackCoinDTOs;
        }

        public async Task<TrackCoinDTO> GetTrackCoinAsync(string coinIdStr)
        {
            TrackCoinDTO? trackCoinDTO = await _trackCoinRepository.GetTrackCoinAsync(coinIdStr);
            return trackCoinDTO;
        }
    }
}
