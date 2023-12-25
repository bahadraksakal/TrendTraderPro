using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TrackCoins
{
    public class TrackCoinDTO
    {
        public string? CoinId { get; set; }

        public TrackStatus? TrackStatus { get; set; }

        public DateTime? LastRequestDate { get; set; }
    }
}
