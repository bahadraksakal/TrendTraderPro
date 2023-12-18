using Entities.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CoinModels
{
    public class SetCoinResponseModel
    {
        public string? Message { get; set; }    
        public List<CoinDTO>? Data { get; set; }
    }
}
