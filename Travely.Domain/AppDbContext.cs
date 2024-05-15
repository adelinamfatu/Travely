using Microsoft.EntityFrameworkCore;
using Travely.Domain.Entities;

namespace Travely.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<TripSqlView> Trips { get; set; }

        public DbSet<FlightSqlView> Flights { get; set; }

        public DbSet<SpotSqlView> Spots { get; set; }

        //public DbSet<PackingItemSqlView> PackingItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripSqlView>().Property(trip => trip.Budget).HasPrecision(18, 2);
            modelBuilder.Entity<SpotSqlView>().Property(spot => spot.EntryFee).HasPrecision(18, 2);
            modelBuilder.Entity<FlightSqlView>().Property(flight => flight.Price).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
