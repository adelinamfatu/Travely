using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class ItineraryViewModel : ObservableObject
    {
        private readonly TripDetailService? tripDetailService;

        [ObservableProperty]
        public Dictionary<string, List<string>> itinerary;

        public ItineraryViewModel(TripDetailService tripDetailService)
        {
            this.tripDetailService = tripDetailService;
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
