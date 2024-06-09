using System.Globalization;
using System.Text.Json;

namespace Travely.Client.Utilities
{
    public class CoordinatesCacheService
    {
        public async Task<(double latitude, double longitude)> GetOrFetchCoordinatesAsync(string country, Func<Task<List<string>>> fetchCoordinatesFunc)
        {
            var cacheFilePath = GetCacheFilePath(country);

            if (File.Exists(cacheFilePath))
            {
                string json = await File.ReadAllTextAsync(cacheFilePath);
                var cachedCoordinates = JsonSerializer.Deserialize<Dictionary<string, List<double>>>(json);

                if (cachedCoordinates is not null && cachedCoordinates.TryGetValue(country, out var coordinates))
                {
                    return (coordinates[0], coordinates[1]);
                }
            }

            var fetchedCoordinates = await fetchCoordinatesFunc();

            if (fetchedCoordinates.Count == 2 && double.TryParse(fetchedCoordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                double.TryParse(fetchedCoordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
            {
                var cachedCoordinates = new Dictionary<string, List<double>> { { country, new List<double> { latitude, longitude } } };
                string json = JsonSerializer.Serialize(cachedCoordinates);
                await File.WriteAllTextAsync(cacheFilePath, json);

                return (latitude, longitude);
            }

            return (0, 0);
        }

        private string GetCacheFilePath(string country)
        {
            var cacheDirectory = FileSystem.CacheDirectory;
            return Path.Combine(cacheDirectory, $"{country}_coordinates.json");
        }
    }
}
