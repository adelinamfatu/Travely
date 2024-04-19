using Travely.Domain.Entities;

namespace Travely.Domain.CRUD
{
    public class TripData
    {
        private AppDbContext context;

        public TripData()
        {
            this.context = new AppDbContext();
        }

        public void AddTrip(TripSqlView trip)
        {
            System.Diagnostics.Debug.WriteLine(trip);
            var existingTrip = this.context.Trips.FirstOrDefault(t => t.Id == trip.Id);

            if (existingTrip == null)
            {
                this.context.Trips.Add(trip);
                this.context.SaveChanges();
            }
        }
    }
}
