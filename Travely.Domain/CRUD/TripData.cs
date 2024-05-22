using Microsoft.EntityFrameworkCore;
using Travely.Domain.Entities;

namespace Travely.Domain.CRUD
{
    public class TripData
    {
        private readonly AppDbContext context;

        public TripData(AppDbContext context)
        {
            this.context = context;
        }

        public void AddTrip(TripSqlView trip)
        {
            var existingTrip = this.context.Trips.FirstOrDefault(t => t.Id == trip.Id);

            if (existingTrip == null)
            {
                this.context.Trips.Add(trip);
                this.context.SaveChanges();
            }
        }

        public void DeleteTrip(Guid tripId)
        {
            var existingTrip = this.context.Trips.FirstOrDefault(t => t.Id == tripId);
            if (existingTrip != null)
            {
                this.context.Trips.Remove(existingTrip);
                this.context.SaveChanges();
            }
        }

        public void UpdateTripNotes(Guid tripId, string notes)
        {
            var existingTrip = this.context.Trips.FirstOrDefault(t => t.Id == tripId);
            if (existingTrip != null)
            {
                existingTrip.Notes = notes;
                this.context.SaveChanges();
            }
        }

        public async Task<List<TripSqlView>> GetTrips()
        {
            return await this.context.Trips.ToListAsync();
        }

        public async Task<TripSqlView?> GetTrip(Guid tripId)
        {
            var trip = await this.context.Trips.Where(trip => trip.Id == tripId).FirstOrDefaultAsync();
            return trip;
        }

        public async Task<List<DateTime>> GetTripDays(Guid tripId)
        {
            var tripDates = await this.context.Trips
                .Where(trip => trip.Id == tripId)
                .Select(trip => new { trip.StartDate, trip.EndDate })
                .ToListAsync();

            return tripDates.SelectMany(trip => new[] { trip.StartDate, trip.EndDate }).ToList();
        }
    }
}
