using Microsoft.EntityFrameworkCore;

namespace GolfBagManagerAPI
{
    public class GolfBagDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }

        public GolfBagDbContext(DbContextOptions<GolfBagDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Table-per-Hierarchy inheritance
            // All club types stored in one table with a discriminator column
            modelBuilder.Entity<Club>()
                .HasDiscriminator<string>("ClubType")
                .HasValue<Driver>("Driver")
                .HasValue<Wood>("Wood")
                .HasValue<Iron>("Iron")
                .HasValue<Wedge>("Wedge")
                .HasValue<Putter>("Putter")
                .HasValue<GenericClub>("Generic");
        }
    }
}