using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private string? notes;

        [ObservableProperty]
        private bool isHotelsExpanded = true;

        [ObservableProperty]
        private bool isNotesExpanded = true;

        [ObservableProperty]
        private bool isDepartureExpanded = true;

        [ObservableProperty]
        private bool isArrivalExpanded = true;

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
            this.Notes = trip.Notes;
        }

        public void ToggleHotelsExpanded() => IsHotelsExpanded = !IsHotelsExpanded;
        public void ToggleNotesExpanded() => IsNotesExpanded = !IsNotesExpanded;
        public void ToggleDepartureExpanded() => IsDepartureExpanded = !IsDepartureExpanded;
        public void ToggleArrivalExpanded() => IsArrivalExpanded = !IsArrivalExpanded;

        [RelayCommand]
        private void AddNotes()
        {
            tripService.UpdateTripNotes(tripId, Notes);
        }
    }
}
