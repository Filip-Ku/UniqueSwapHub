using Microsoft.EntityFrameworkCore;
using HubApi.Models;

namespace HubApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemDetails> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("itemhub");

            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.createdAt)
                .HasColumnType("timestamp with time zone");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.reservationTime)
                .HasColumnType("timestamp with time zone");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.expireDate)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<User>().HasKey(u => u.userId);

            modelBuilder.Entity<Reservation>().HasKey(r => r.reservationId);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            UseLowerCaseNames(modelBuilder);
        }


          private void UseLowerCaseNames(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName()?.ToLower());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName()?.ToLower());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName()?.ToLower());
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.SetConstraintName(fk.GetConstraintName()?.ToLower());
                }
            }
        }
    }
}