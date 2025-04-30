using Microsoft.EntityFrameworkCore;

namespace ItemHubApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemDetails> Items { get; set; }
    }
}
