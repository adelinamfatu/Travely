using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        [ObservableProperty]
        private Guid tripId;

        [ObservableProperty]
        private string? currentSpotName;

        [ObservableProperty]
        public Dictionary<string, List<string>> itinerary;

        private Dictionary<string, DateTime> tripDaysDates;

        private double? currentLongitude;

        private double? currentLatitude;

        private string? currentAddress;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            Itinerary = new Dictionary<string, List<string>>();
            tripDaysDates = new Dictionary<string, DateTime>();
        }

        public async Task InitializeItinerary(Guid tripId)
        {
            this.TripId = tripId;

            if (tripDetailService is not null)
            {
                var tripDays = await tripDetailService.GetTripDays(tripId);
                int dayCount = 1;

                foreach (var day in tripDays)
                {
                    string dayTitle = $"Day {dayCount} ({day:dd.MM.yyyy})";

                    if (!Itinerary.ContainsKey(dayTitle))
                    {
                        Itinerary.Add(dayTitle, new List<string>());
                    }

                    tripDaysDates[dayTitle] = day;
                    dayCount++;
                }
            }
        }

        [RelayCommand]
        private void AddSpot(string dayTitle)
        {
            if (tripDetailService is not null && CurrentSpotName is not null)
            {
                if (tripDaysDates.TryGetValue(dayTitle, out DateTime day))
                {
                    if (Itinerary.ContainsKey(dayTitle))
                    {
                        Itinerary[dayTitle].Add(CurrentSpotName);

                        tripDetailService.AddSpot(new SpotDTO
                        {
                            Name = CurrentSpotName,
                            Latitude = currentLatitude,
                            Longitude = currentLongitude,
                            Address = currentAddress,
                            Date = day
                        }, TripId);
                    }
                }
            }
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
}
