using AutoMapper;

namespace Entities.Coins
{
    public class CoinProfile : Profile
    {
        public CoinProfile()
        {
            CreateMap<CoinDTO, Coin>();
            CreateMap<Coin, CoinDTO>();

            CreateMap<CoinGeckoApiDTO, Coin >()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.symbol))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));
            CreateMap<Coin, CoinGeckoApiDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name));
        }

    }
}
