using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Travely.Client.Resources.UIResources;
using System.Windows.Input;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using static Travely.Client.Utilities.Messenger;
using Travely.Client.Utilities;

namespace Travely.Client.Models
{
    public partial class TripViewModel : ObservableObject
    {
        private readonly TripService? tripService;

        private List<string>? Countries { get; set; }

        public Guid Id { get; set; }

        [ObservableProperty]
        private string? tripTitle;

        [ObservableProperty]
        private string? countryName;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddDays(1);

        public string? CountryURL { get; set; }

        [ObservableProperty]
        private string addTripMessage = "";

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
            InitializeCountries();
        }

        private async void InitializeCountries()
        {
            if (tripService is not null)
            {
                Countries = await tripService.GetWorldCountries(Constants.Continents);
            }
        }

        public TripViewModel(TripService tripService)
        {
            this.tripService = tripService;
            InitializeCommands();
            InitializeCountries();
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
                AddTripMessage = ValidationResources.AddTripSuccess;
            }
            else
            {
                if (string.IsNullOrEmpty(TripTitle))
                {
                    AddTripMessage = ValidationResources.EmptyTitleError;
                }
                else if (!IsTitleValid(TripTitle))
                {
                    AddTripMessage = ValidationResources.InvalidTitleError;
                }
                else if (string.IsNullOrEmpty(CountryName))
                {
                    AddTripMessage = ValidationResources.InvalidDestinationError;
                }
                else if (StartDate < DateTime.Today)
                {
                    AddTripMessage = ValidationResources.InvalidStartDate;
                }
                else if (EndDate < DateTime.Today || StartDate > EndDate)
                {
                    AddTripMessage = ValidationResources.InvalidEndDate;
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
