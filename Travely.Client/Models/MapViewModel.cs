using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Globalization;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;

namespace Travely.Client.Models
{
    public partial class MapViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        private Guid? tripId;

        [ObservableProperty]
        private string? country;

        public double CountryLatitude { get; private set; }
        public double CountryLongitude { get; private set; }

        public MapViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
        }

        public async Task InitializeCountry(Guid tripId)
        {
            this.tripId = tripId;

            if (tripDetailService is not null)
            {
                this.Country = await tripDetailService.GetTripCountry(tripId);
                await InitializeCoordinates();
            }
        }

        private async Task InitializeCoordinates()
        {
            if (!string.IsNullOrEmpty(Country) && tripDetailService is not null)
            {
                var cacheDirectory = FileSystem.CacheDirectory;
                var cacheFilePath = Path.Combine(cacheDirectory, Constants.CoordinatesCacheFileName);

                if (File.Exists(cacheFilePath))
                {
                    string json = await File.ReadAllTextAsync(cacheFilePath);
                    var cachedCoordinates = JsonSerializer.Deserialize<Dictionary<string, List<double>>>(json);

                    if (cachedCoordinates is not null && cachedCoordinates.TryGetValue(Country, out var coordinates))
                    {
                        CountryLatitude = coordinates[0];
                        CountryLongitude = coordinates[1];
                    }
                    else
                    {
                        await FetchAndCacheCoordinates(cacheFilePath);
                    }
                }
                else
                {
                    await FetchAndCacheCoordinates(cacheFilePath);
                }
            }
        }

        private async Task FetchAndCacheCoordinates(string cacheFilePath)
        {
            if (tripDetailService is not null && Country is not null)
            {
                var coordinates = await tripDetailService.GetCountryCoordinates(Country);
                if (coordinates.Count == 2)
                {
                    if (double.TryParse(coordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(coordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                    {
                        CountryLatitude = latitude;
                        CountryLongitude = longitude;

                        var cachedCoordinates = new Dictionary<string, List<double>> { { Country, new List<double> { latitude, longitude } } };
                        string json = JsonSerializer.Serialize(cachedCoordinates);
                        await File.WriteAllTextAsync(cacheFilePath, json);
                    }
                }
            }
        }
    }
}
