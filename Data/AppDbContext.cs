using Microsoft.EntityFrameworkCore;

namespace asp.net_api_teaching.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}
    }
}
