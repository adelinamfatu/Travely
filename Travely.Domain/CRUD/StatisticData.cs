namespace Travely.Domain.CRUD
{
    public class StatisticData
    {
        private readonly AppDbContext context;

        public StatisticData(AppDbContext context)
        {
            this.context = context;
        }

        public Dictionary<string, int> GetTripCountries()
        {
            var tripStatistics = context.Trips
                .Where(trip => trip.Country != null)
                .GroupBy(trip => trip.Country!)
                .Select(group => new { Country = group.Key, Count = group.Count() })
                .ToDictionary(x => x.Country!, x => x.Count);

            return tripStatistics;
        }
    }
}
