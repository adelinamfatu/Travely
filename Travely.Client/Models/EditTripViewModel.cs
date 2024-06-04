using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.RegularExpressions;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using Travely.Client.Resources.UIResources;
using Travely.Domain.Entities;
using static Travely.Client.Utilities.Messenger;

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
        private bool isNotesExpanded = true;

        [ObservableProperty]
        private bool isDepartureExpanded = true;

        [ObservableProperty]
        private bool isArrivalExpanded = true;

        [ObservableProperty]
        private string alertMessage = "";

        [ObservableProperty]
        private string alertDepartureFlight = "";

        [ObservableProperty]
        private string alertArrivalFlight = "";

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
            this.alertDepartureFlight = string.Empty;
            this.alertArrivalFlight = string.Empty;
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

        public void ToggleNotesExpanded() => IsNotesExpanded = !IsNotesExpanded;
        public void ToggleDepartureExpanded() => IsDepartureExpanded = !IsDepartureExpanded;
        public void ToggleArrivalExpanded() => IsArrivalExpanded = !IsArrivalExpanded;

        [RelayCommand]
        private void AddNotes()
        {
            AlertMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Notes))
            {
                AlertMessage = ValidationResources.EmptyNoteError;
                return;
            }

            if (!Regex.IsMatch(Notes, @"^[a-zA-Z0-9 ]+$"))
            {
                AlertMessage = ValidationResources.InvalidNoteError;
                return;
            }

            Notes = FormatNoteText(Notes);
            tripService.UpdateTripNotes(TripId, Notes);
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
        private async Task SearchDepartureFlight()
        {
            AlertDepartureFlight = string.Empty;
            if (string.IsNullOrWhiteSpace(DepartureFlightNumber))
            {
                AlertDepartureFlight = ValidationResources.EmptyDepartureFlightError;
                return;
            }

            if (!Regex.IsMatch(DepartureFlightNumber, @"^\d{8}$"))
            {
                AlertDepartureFlight = ValidationResources.InvalidDepartureFlightError;
                return;
            }

            if (DepartureFlight is null)
            {
                await tripService.AddFlightDetails(DepartureFlightNumber, FlightType.Departure, TripId);
            }
            else
            {
                await tripService.UpdateFlightDetails(DepartureFlightNumber);
            }

            WeakReferenceMessenger.Default.Send(new ReloadFlightsMessage());
        }

        [RelayCommand]
        private async Task SearchArrivalFlight()
        {
            AlertArrivalFlight = string.Empty;
            if (string.IsNullOrWhiteSpace(ArrivalFlightNumber))
            {
                AlertArrivalFlight = ValidationResources.EmptyArrivalFlightError;
                return;
            }

            if (!Regex.IsMatch(ArrivalFlightNumber, @"^\d{8}$"))
            {
                AlertArrivalFlight = ValidationResources.InvalidArrivalFlightError;
                return;
            }

            if (ArrivalFlight is null)
            {
                await tripService.AddFlightDetails(ArrivalFlightNumber, FlightType.Arrival, TripId);
            }
            else
            {
                await tripService.UpdateFlightDetails(ArrivalFlightNumber);
            }

            WeakReferenceMessenger.Default.Send(new ReloadFlightsMessage());
        }
    }
}
