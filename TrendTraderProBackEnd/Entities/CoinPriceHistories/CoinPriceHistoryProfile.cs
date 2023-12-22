using AutoMapper;

namespace Entities.CoinPriceHistories
{
    public class CoinPriceHistoryProfile : Profile
    {
        public CoinPriceHistoryProfile()
        {
            CreateMap<CoinPriceHistory, CoinPriceHistoryDTO>();
            CreateMap<CoinPriceHistoryDTO, CoinPriceHistory>();
        }
    }
}
