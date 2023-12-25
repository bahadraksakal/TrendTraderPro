using Entities.TrackCoins;

namespace Repositorys.TrackCoinRepository
{
    public interface ITrackCoinRepository
    {
        Task<TrackCoinDTO> AddTrackCoinAsync(string coinIdStr, DateTime lastRequestDate, TrackStatus trackStatus);
        Task<TrackCoinDTO> GetTrackCoinAsync(string coinIdStr);
        Task<List<TrackCoinDTO>> GetAllTrackCoinsAsync();
        Task<TrackCoinDTO> UpdateTrackCoin(string coinIdStr, DateTime newLastRequestDate, TrackStatus? trackStatus = TrackStatus.Tracked);
        Task<List<TrackCoinDTO>> GetAllTrackCoinsOnlyTrackedAsync();
    }
}
