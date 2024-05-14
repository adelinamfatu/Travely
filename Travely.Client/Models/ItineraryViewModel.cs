using CommunityToolkit.Mvvm.ComponentModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripService? tripService;

        [ObservableProperty]
        public Dictionary<string, List<string>> itinerary;

        public ItineraryViewModel(TripService tripService)
        {
            this.tripService = tripService;
            Itinerary = new Dictionary<string, List<string>>();
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
    }
}
