using CommunityToolkit.Mvvm.ComponentModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class MapViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        private Guid? tripId;

        [ObservableProperty]
        private string? country;

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

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
                var coordinates = await tripDetailService.GetCountryCoordinates(Country);
                if (coordinates.Count == 2)
                {
                    Latitude = double.Parse(coordinates[0]);
                    Longitude = double.Parse(coordinates[1]);
                }
            }
        }
    }
}
