using Microsoft.EntityFrameworkCore;
using HubApi.Models;

namespace HubApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemDetails> Items { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemDetails>().ToTable("items", "itemhub");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.createdAt)
                .HasColumnType("timestamp with time zone");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.reservationTime)
                .HasColumnType("timestamp with time zone");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.expireDate)
                .HasColumnType("timestamp with time zone");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            modelBuilder.Entity<User>().ToTable("users", "itemhub");
            modelBuilder.Entity<User>().HasKey(u => u.userId);
        }
    }
}