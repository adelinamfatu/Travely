using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        private Guid? tripId;

        [ObservableProperty]
        public Dictionary<string, List<string>> itinerary;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
            Itinerary = new Dictionary<string, List<string>>();
        }

        public async Task InitializeItinerary(Guid tripId)
        {
            this.tripId = tripId;

            if (tripDetailService is not null)
            {
                var tripDays = await tripDetailService.GetTripDays(tripId);
                int dayCount = 1;

                foreach (var day in tripDays)
                {
                    string dayTitle = $"Day {dayCount} ({day:yyyy-MM-dd})";
                    AddDay(dayTitle);
                    dayCount++;
                }
            }
        }

        public void AddDay(string dayTitle)
        {
            if (!Itinerary.ContainsKey(dayTitle))
                Itinerary.Add(dayTitle, new List<string>());
        }

        public void AddPlace(string dayTitle, string place)
        {
            if (Itinerary.ContainsKey(dayTitle))
                Itinerary[dayTitle].Add(place);
        }

        [RelayCommand]
        private void AddSpot()
        {

        }
    }
}
