using Microsoft.EntityFrameworkCore;

namespace ChickenAPI.Model
{
    public class FarmDbContext : DbContext
    {
        public DbSet<Chicken> Chickens { get; set; }

        public FarmDbContext(DbContextOptions<FarmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chicken>()
                .ToTable("Chicken");

            modelBuilder.Entity<Chicken>()
                .Property(c => c.EggProduction)
                .HasPrecision(5, 2);
        }
    }
}
