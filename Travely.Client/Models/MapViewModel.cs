using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Globalization;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;
using CommunityToolkit.Mvvm.Input;
using Travely.Client.Resources.UIResources;

namespace Travely.Client.Models
{
    public partial class MapViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;
        private readonly CoordinatesCacheService coordinatesCacheService;

        private Guid? tripId;

        [ObservableProperty]
        private string? country;

        [ObservableProperty]
        private string? location;

        public event EventHandler<Tuple<double, double>>? MoveMapToRegion;

        public double CountryLatitude { get; private set; }
        public double CountryLongitude { get; private set; }

        public MapViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            this.coordinatesCacheService = new CoordinatesCacheService();
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
                var (latitude, longitude) = await coordinatesCacheService.GetOrFetchCoordinatesAsync(Country, () => tripDetailService.GetPlaceCoordinates(Country));
                CountryLatitude = latitude;
                CountryLongitude = longitude;
            }
        }

        [RelayCommand]
        private async Task SearchPlace()
        {
            if (this.Location is not null && tripDetailService is not null)
            {
                var coordinates = await tripDetailService.GetPlaceCoordinates(Location);
                if (coordinates.Count == 2)
                {
                    if (double.TryParse(coordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(coordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                    {
                        MoveMapToRegion?.Invoke(this, new Tuple<double, double>(latitude, longitude));
                    }
                }
                else
                {
                    await ShowAlert(ValidationResources.ErrorLocationTitle, ValidationResources.ErrorLocation, ValidationResources.OK);
                }
            }
        }

        public Task ShowAlert(string title, string message, string cancel)
        {
            return MainThread.InvokeOnMainThreadAsync(() =>
                Application.Current?.MainPage?.DisplayAlert(title, message, cancel));
        }
    }
}
