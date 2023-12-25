using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TrackCoins
{
    public class TrackCoinProfile : Profile
    {
        public TrackCoinProfile()
        {
            CreateMap<TrackCoin, TrackCoinDTO>();
            CreateMap<TrackCoinDTO, TrackCoin>();
        }
    }
}
