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
    }
}
