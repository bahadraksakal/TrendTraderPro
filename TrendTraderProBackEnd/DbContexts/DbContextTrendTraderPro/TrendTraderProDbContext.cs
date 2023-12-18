using Entities.Coins;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DbContexts.DbContextTrendTraderPro
{
    public class TrendTraderProDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Coin> Coins { get; set; }

        public TrendTraderProDbContext(DbContextOptions<TrendTraderProDbContext> options) : base(options)
        {
        }
    }
}
