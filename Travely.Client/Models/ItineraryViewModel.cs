using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using System.Collections.ObjectModel;
using static Travely.Client.Utilities.Messenger;
using Travely.Client.Utilities;
using Travely.Client.Resources.UIResources;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;
        private readonly CoordinatesCacheService coordinatesCacheService;

        [ObservableProperty]
        private Guid tripId;

        [ObservableProperty]
        private string? currentSpotName;

        [ObservableProperty]
        private decimal totalFee;

        [ObservableProperty]
        private WeatherDTO? weather;

        [ObservableProperty]
        private string entryFeeErrorMessage = "";

        [ObservableProperty]
        private ObservableCollection<DayItinerary> itinerary;

        private Dictionary<string, DateTime> tripDaysDates;

        private double? currentLongitude;

        private double? currentLatitude;

        private string? currentAddress;

        private string? country;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            this.coordinatesCacheService = new CoordinatesCacheService();
            Itinerary = new ObservableCollection<DayItinerary>();
            tripDaysDates = new Dictionary<string, DateTime>();
            TotalFee = 0m;
        }

        public async Task InitializeCountry(Guid tripId)
        {
            this.TripId = tripId;

            if (tripDetailService is not null)
            {
                this.country = await tripDetailService.GetTripCountry(tripId);
                await InitializeCoordinates();
            }
        }

        private async Task InitializeCoordinates()
        {
            if (!string.IsNullOrEmpty(country) && tripDetailService is not null)
            {
                var (latitude, longitude) = await coordinatesCacheService.GetOrFetchCoordinatesAsync(country, () => tripDetailService.GetPlaceCoordinates(country));
                await LoadWeather(latitude, longitude);
            }
        }

        private async Task LoadWeather(double latitude, double longitude)
        {
            if (tripDetailService is not null)
            {
                this.Weather = await tripDetailService.GetCountryWeather(latitude, longitude);
            }
        }

        public async Task LoadItinerary(Guid tripId)
        {
            this.TripId = tripId;
            Itinerary.Clear();

            if (tripDetailService is not null)
            {
                var tripSpots = await tripDetailService.GetTripSpots(tripId);
                var spotsByDay = tripSpots.GroupBy(spot => spot.Date.Date).ToDictionary(g => g.Key, g => g.ToList());

                var tripDays = await tripDetailService.GetTripDays(tripId);
                int dayCount = 1;

                foreach (var day in tripDays)
                {
                    string dayTitle = $"Day {dayCount} - {day:dd.MM.yyyy}";

                    var dayItinerary = new DayItinerary(dayTitle, new ObservableCollection<SpotDTO>());

                    tripDaysDates[dayTitle] = day;

                    if (spotsByDay.TryGetValue(day.Date, out var spots))
                    {
                        foreach (var spot in spots)
                        {
                            if (spot is not null)
                            {
                                dayItinerary.Spots.Add(spot);
                            }
                        }
                    }

                    Itinerary.Add(dayItinerary);
                    dayCount++;
                }

                TotalFee = Itinerary.SelectMany(day => day.Spots).Sum(spot => spot.EntryFee ?? 0m);
            }
        }

        public void AddSpot(string dayTitle)
        {
            if (tripDetailService is not null && CurrentSpotName is not null)
            {
                if (tripDaysDates.TryGetValue(dayTitle, out DateTime day))
                {
                    var dayItinerary = Itinerary.FirstOrDefault(it => it.DayTitle == dayTitle);
                    if (dayItinerary != null)
                    {
                        tripDetailService.AddSpot(new SpotDTO
                        {
                            Name = CurrentSpotName,
                            Latitude = currentLatitude,
                            Longitude = currentLongitude,
                            Address = currentAddress,
                            Date = day
                        }, TripId);

                        WeakReferenceMessenger.Default.Send(new ReloadSpotsMessage());
                    }
                }
            }
        }

        [RelayCommand]
        public void DeleteSpot(SpotDTO spot)
        {
            if (tripDetailService != null)
            {
                tripDetailService.DeleteSpot(spot.Id);
                WeakReferenceMessenger.Default.Send(new ReloadSpotsMessage());
            }
        }

        [RelayCommand]
        public void UpdateSpotTime(SpotDTO spot)
        {
            if (tripDetailService != null)
            {
                var time = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, spot.Time.Hours, spot.Time.Minutes, spot.Time.Seconds);
                tripDetailService.UpdateSpotTime(spot.Id, time);
            }
        }

        [RelayCommand]
        public void UpdateSpotFee(SpotDTO spot)
        {
            if (tripDetailService != null && spot.EntryFee != null)
            {
                string? feeInput = spot.EntryFee.ToString();
                if (!IsValidFee(feeInput))
                {
                    EntryFeeErrorMessage = ValidationResources.EntryFeeError;
                    return;
                }

                decimal entryFee = spot.EntryFee ?? 0m;
                tripDetailService.UpdateSpotFee(spot.Id, entryFee);
                TotalFee = Itinerary.SelectMany(day => day.Spots).Sum(spot => spot.EntryFee ?? 0m);
                EntryFeeErrorMessage = ""; 
            }
        }

        private bool IsValidFee(string? feeInput)
        {
            if (decimal.TryParse(feeInput, out decimal result))
            {
                var parts = feeInput.Split('.');
                if (parts.Length > 1 && parts[1].Length > 2)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public async void GetSpotData(double latitude, double longitude)
        {
            if (tripDetailService is not null)
            {
                this.currentLongitude = longitude;
                this.currentLatitude = longitude;
                this.currentAddress = await tripDetailService.GetSpotDetails(latitude, longitude);
                this.CurrentSpotName = currentAddress.Split(',')[0];
            }
        }
    }

    public partial class DayItinerary : ObservableObject
    {
        public DayItinerary(string dayTitle, ObservableCollection<SpotDTO> spots)
        {
            DayTitle = dayTitle;
            Spots = spots;
        }

        public string DayTitle { get; }

        [ObservableProperty]
        private ObservableCollection<SpotDTO> spots;
    }
}