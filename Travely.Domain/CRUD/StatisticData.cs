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

        public Dictionary<string, double> GetSeasonalSpending()
        {
            var spots = context.Spots
                .Where(spot => spot.EntryFee.HasValue)
                .Select(spot => new { spot.Date, spot.EntryFee })
                .ToList();

            var seasonalSpending = spots
                .GroupBy(spot => GetSeason(spot.Date))
                .Select(group => new { Season = group.Key, TotalSpending = group.Sum(spot => (double)spot.EntryFee!) })
                .ToDictionary(x => x.Season, x => x.TotalSpending);

            return seasonalSpending;
        }

        private string GetSeason(DateTime date)
        {
            if (date.Month == 12 || date.Month == 1 || date.Month == 2)
                return "Winter";
            else if (date.Month == 3 || date.Month == 4 || date.Month == 5)
                return "Spring";
            else if (date.Month == 6 || date.Month == 7 || date.Month == 8)
                return "Summer";
            else
                return "Fall";
        }
    }
}
