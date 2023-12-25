using Entities.CoinPriceHistories;
using Entities.Coins;
using Entities.TrackCoins;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DbContexts.DbContextTrendTraderPro
{
    public class TrendTraderProDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<CoinPriceHistory> CoinPriceHistories { get; set; } 
        public virtual DbSet<TrackCoin> TrackCoins { get; set; }

        public TrendTraderProDbContext(DbContextOptions<TrendTraderProDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User userAdmin = new User
            {
                Id = 1,
                Name = "admin",
                Password = "admin",
                Email = "admin@gmail.com",
                Tel = "05350449876",
                Role = "Admin"
            };
            modelBuilder.Entity<User>().HasData(userAdmin);
        }
    }
}
