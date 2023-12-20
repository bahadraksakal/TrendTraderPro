using Microsoft.EntityFrameworkCore;

namespace DbContexts.DbContextHangFire
{
    public class HangFireDbContext : DbContext
    {
        public HangFireDbContext(DbContextOptions<HangFireDbContext> options) : base(options)
        {
        }
    }
}
