using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using System.Windows.Input;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Models
{
    public partial class TripViewModel : ObservableObject
    {
        private readonly TripService? tripService;

        [ObservableProperty]
        private string? tripTitle;

        [ObservableProperty]
        private string? countryName;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today;

        public Guid Id { get; set; }
        public string? CountryURL { get; set; }

        [ObservableProperty]
        private string lastAddTripMessage = "";

        public ICommand? DeleteCommand { get; private set; }
        public ICommand? AddTripCommand { get; private set; }

        public TripViewModel(TripDTO trip, TripService tripService)
        {
            this.Id = trip.Id;
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate;
            this.EndDate = trip.EndDate;
            this.TripTitle = trip.Title;
            this.tripService = tripService;
            InitializeCommands();
        }

        public TripViewModel(TripService tripService)
        {
            this.tripService = tripService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            DeleteCommand = new RelayCommand<Guid>(ExecuteDeleteCommand);
            AddTripCommand = new RelayCommand(AddTrip, CanAddTrip);
        }

        private bool CanAddTrip() => !string.IsNullOrEmpty(TripTitle) &&
                                     IsTitleValid(TripTitle) &&
                                     !string.IsNullOrEmpty(CountryName) &&
                                     StartDate <= EndDate &&
                                     StartDate >= DateTime.Today &&
                                     EndDate >= StartDate;

        private bool IsTitleValid(string title)
        {
            return title.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
        }

        public void AddTrip()
        {
            if (tripService != null && CanAddTrip())
            {
                tripService.AddTrip(new TripDTO
                {
                    Title = TripTitle,
                    Country = CountryName,
                    StartDate = StartDate,
                    EndDate = EndDate
                });
                WeakReferenceMessenger.Default.Send(new ReloadTripsMessage());
                LastAddTripMessage = "Trip added successfully.";
            }
            else
            {
                if (string.IsNullOrEmpty(TripTitle))
                {
                    LastAddTripMessage = "Title cannot be empty.";
                }
                else if (!IsTitleValid(TripTitle))
                {
                    LastAddTripMessage = "Title must contain only letters, numbers, and spaces.";
                }
                else if (string.IsNullOrEmpty(CountryName))
                {
                    LastAddTripMessage = "Destination cannot be empty and must contain only letters.";
                }
                else if (StartDate < DateTime.Today)
                {
                    LastAddTripMessage = "Start date is not valid.";
                }
                else if (EndDate < DateTime.Today)
                {
                    LastAddTripMessage = "End date is not valid.";
                }
                else if (StartDate > EndDate)
                {
                    LastAddTripMessage = "End date is not valid.";
                }
            }
        }

        private void ExecuteDeleteCommand(Guid tripId)
        {
            if (tripService != null)
            {
                tripService.DeleteTrip(tripId);
                WeakReferenceMessenger.Default.Send(new ReloadTripsMessage());
            }
        }
    }
}
