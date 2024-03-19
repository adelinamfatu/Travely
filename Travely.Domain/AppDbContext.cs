using Microsoft.EntityFrameworkCore;
using Travely.Domain.Entities;

namespace Travely.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserSqlView> Users { get; set; }
        public DbSet<FlightSqlView> Flights { get; set; }
        public DbSet<TouristSpotSqlView> TouristSpots { get; set; }
        public DbSet<BudgetSqlView> Budgets { get; set; }
        public DbSet<BudgetFlightSqlView> BudgetFlights { get; set; }
        public DbSet<BudgetTouristSpotSqlView> BudgetTouristSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BudgetFlightSqlView>()
                .HasKey(bf => new { bf.BudgetId, bf.FlightId });
            modelBuilder.Entity<BudgetFlightSqlView>()
                .HasOne(bf => bf.Budget)
                .WithMany(b => b.BudgetFlights)
                .HasForeignKey(bf => bf.BudgetId);
            modelBuilder.Entity<BudgetFlightSqlView>()
                .HasOne(bf => bf.Flight)
                .WithMany(f => f.BudgetFlights)
                .HasForeignKey(bf => bf.FlightId);

            modelBuilder.Entity<BudgetTouristSpotSqlView>()
                .HasKey(bts => new { bts.BudgetId, bts.SpotId });
            modelBuilder.Entity<BudgetTouristSpotSqlView>()
                .HasOne(bts => bts.Budget)
                .WithMany(b => b.BudgetTouristSpots)
                .HasForeignKey(bts => bts.BudgetId);
            modelBuilder.Entity<BudgetTouristSpotSqlView>()
                .HasOne(bts => bts.TouristSpot)
                .WithMany(ts => ts.BudgetTouristSpots)
                .HasForeignKey(bts => bts.SpotId);
        }
    }
}
