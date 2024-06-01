using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class EditTripViewModel : ObservableObject
    {
        private readonly TripService tripService;

        [ObservableProperty]
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
        private string? countryFlagUrl;

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

        [ObservableProperty]
        private string alertMessage;

        [ObservableProperty]
        private string? arrivalFlightNumber;

        [ObservableProperty]
        private string? departureFlightNumber;

        [ObservableProperty]
        private FlightDTO? arrivalFlight;

        [ObservableProperty]
        private FlightDTO? departureFlight;

        public EditTripViewModel(Guid tripId, TripService tripService)
        {
            this.tripService = tripService;
            this.TripId = tripId;
            this.alertMessage = string.Empty;
        }

        public async Task LoadTrip()
        {
            var trip = await tripService.GetTrip(TripId);
            this.TripTitle = trip.Title;
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate;
            this.EndDate = trip.EndDate;
            this.Notes = trip.Notes;
            SetCountryFlagUrl();
            SetFlights();
        }

        public void ToggleHotelsExpanded() => IsHotelsExpanded = !IsHotelsExpanded;
        public void ToggleNotesExpanded() => IsNotesExpanded = !IsNotesExpanded;
        public void ToggleDepartureExpanded() => IsDepartureExpanded = !IsDepartureExpanded;
        public void ToggleArrivalExpanded() => IsArrivalExpanded = !IsArrivalExpanded;

        [RelayCommand]
        private void AddNotes()
        {
            AlertMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Notes))
            {
                AlertMessage = "Item title cannot be empty.";
                return;
            }

            if (!Regex.IsMatch(Notes, @"^[a-zA-Z0-9 ]+$"))
            {
                AlertMessage = "Item title can only contain letters, digits and spaces.";
                return;
            }

            Notes = FormatNoteText(Notes);
            tripService.UpdateTripNotes(tripId, Notes);
        }

        private string FormatNoteText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        private void SetCountryFlagUrl()
        {
            if (tripService != null && !string.IsNullOrEmpty(CountryName))
            {
                var countryCode = CountryName.Substring(0, 2).ToLower();
                CountryFlagUrl = tripService.GetFlagUrl(countryCode);
            }
        }

        private async void SetFlights()
        {
            var flights = await this.tripService.GetFlights(TripId);
            if (flights != null)
            {
                foreach (var flight in flights)
                {
                    if (flight.FlightType == Domain.Entities.FlightType.Departure)
                    {
                        this.DepartureFlight = flight;
                    }
                    else
                    {
                        this.ArrivalFlight = flight;
                    }
                }
            }
        }

        [RelayCommand]
        private void SearchArrivalFlight()
        {
            tripService.GetFlightDetails(arrivalFlightNumber, TripId);
        }

        [RelayCommand]
        private void SearchDepartureFlight()
        {
            tripService.GetFlightDetails(departureFlightNumber, TripId);
        }
    }
}
