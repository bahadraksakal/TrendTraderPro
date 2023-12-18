using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Coins
{
    public class CoinDTO
    {
        public string? Id { get; set; }

        public string? Symbol { get; set; }
        
        public string? Name { get; set; }
    }
}
