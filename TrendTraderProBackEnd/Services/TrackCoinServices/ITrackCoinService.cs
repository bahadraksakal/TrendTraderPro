using Entities.TrackCoins;

namespace Services.TrackCoinServices
{
    public interface ITrackCoinService
    {
        Task<TrackCoinDTO> SetTrackCoinAsync(string coinIdStr);
        Task<TrackCoinDTO> SetUnTrackCoinToLastRequestAsync(string coinIdStr);
        Task<List<TrackCoinDTO>> GetAllTrackCoinsAsync();
        Task<TrackCoinDTO> GetTrackCoinAsync(string coinIdStr);
    }
}
