using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Entities.Coins
{
    public class Coin
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string? Symbol { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
