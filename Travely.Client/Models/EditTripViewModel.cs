using CommunityToolkit.Mvvm.ComponentModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class EditTripViewModel : ObservableObject
    {
        private readonly TripService? tripService;

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
        }
    }
}
