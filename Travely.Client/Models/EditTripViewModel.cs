using CommunityToolkit.Mvvm.ComponentModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class EditTripViewModel : ObservableObject
    {
        private readonly TripService tripService;

        private Guid tripId;

        [ObservableProperty]
        private string? tripTitle;

        [ObservableProperty]
        private string? countryName;

        [ObservableProperty]
        private DateTime startDate;

        [ObservableProperty]
        private DateTime endDate;

        public EditTripViewModel(Guid tripId, TripService tripService)
        {
            this.tripService = tripService;
            this.tripId = tripId;
        }

        public async Task LoadTrip()
        {
            var trip = await tripService.GetTrip(tripId);
            this.TripTitle = trip.Title;
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate;
            this.EndDate = trip.EndDate;
        }
    }
}
