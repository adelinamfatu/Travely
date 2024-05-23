using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Travely.Client.Resources.UIResources;
using System.Windows.Input;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using static Travely.Client.Utilities.Messenger;
using Travely.Client.Utilities;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Travely.Client.Models
{
    public partial class TripViewModel : ObservableObject
    {
        private readonly TripService? tripService;

        public ObservableCollection<string> Countries { get; } = new ObservableCollection<string>();

        public Guid Id { get; set; }

        [ObservableProperty]
        private string? tripTitle;

        [ObservableProperty]
        private string? countryName;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private string? countryFlagUrl;

        [ObservableProperty]
        private string addTripMessage = "";

        public ICommand? DeleteTripCommand { get; private set; }
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
            SetCountryFlagUrl();
        }

        public TripViewModel(TripService tripService)
        {
            this.tripService = tripService;
            InitializeCommands();
            InitializeCountries();
        }

        private void InitializeCommands()
        {
            DeleteTripCommand = new RelayCommand<Guid>(ExecuteDeleteCommand);
            AddTripCommand = new RelayCommand(AddTrip, CanAddTrip);
        }

        private async void InitializeCountries()
        {
            List<string>? countryNames = new List<string>();

            var cacheDirectory = FileSystem.CacheDirectory;
            var cacheFilePath = Path.Combine(cacheDirectory, Constants.CountriesCacheFileName);

            if (File.Exists(cacheFilePath))
            {
                string json = await File.ReadAllTextAsync(cacheFilePath);
                countryNames = JsonSerializer.Deserialize<List<string>>(json);
            }
            else
            {
                if (tripService is not null)
                {
                    countryNames = await tripService.GetWorldCountries(Constants.Continents);
                    string json = JsonSerializer.Serialize(countryNames);
                    await File.WriteAllTextAsync(cacheFilePath, json);
                }
            }

            if (countryNames is not null)
            {
                foreach (var name in countryNames)
                {
                    Countries.Add(name);
                }
            }
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

        public void ExecuteDeleteCommand(Guid tripId)
        {
            if (Id != Guid.Empty && tripService != null)
            {
                tripService.DeleteTrip(Id);
                WeakReferenceMessenger.Default.Send(new ReloadTripsMessage());
            }
        }

        private void SetCountryFlagUrl()
        {
            if (tripService != null && !string.IsNullOrEmpty(CountryName))
            {
                var countryCode = CountryName.Substring(0, 2).ToLower();
                CountryFlagUrl = tripService.GetFlagUrl(countryCode);
            }
        }
    }
}
