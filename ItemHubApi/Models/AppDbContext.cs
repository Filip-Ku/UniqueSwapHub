using Microsoft.EntityFrameworkCore;

namespace HubApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemDetails> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.CreatedAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.reservationTime)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.ExpireDate)
                .HasColumnType("timestamp with time zone");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}