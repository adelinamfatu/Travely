using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        [ObservableProperty]
        private Guid? tripId;

        private string? spotName;

        [ObservableProperty]
        public Dictionary<string, List<string>> itinerary;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            Itinerary = new Dictionary<string, List<string>>();
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
                    string dayTitle = $"Day {dayCount} ({day:yyyy-MM-dd})";

                    if (!Itinerary.ContainsKey(dayTitle))
                    {
                        Itinerary.Add(dayTitle, new List<string>());
                    }
                        
                    dayCount++;
                }
            }
        }

        [RelayCommand]
        private void AddSpot()
        {

        }

        public async void GetSpotData(double latitude, double longitude)
        {
            if (tripDetailService is not null)
            {
                this.spotName = await tripDetailService.GetSpotName(latitude, longitude);
            }
        }
    }
}
