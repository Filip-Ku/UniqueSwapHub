using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.createdAt)
                .HasColumnType("datetime");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.reservationTime)
                .HasColumnType("datetime");
            modelBuilder.Entity<ItemDetails>()
                .Property(x => x.expireDate)
                .HasColumnType("datetime");

            modelBuilder.Entity<User>().HasKey(u => u.userId);

            modelBuilder.Entity<Reservation>().HasKey(r => r.reservationId);


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