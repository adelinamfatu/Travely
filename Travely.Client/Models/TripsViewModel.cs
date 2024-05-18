using System.Collections.ObjectModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class TripsViewModel
    {
        private readonly TripService tripService;

        public ObservableCollection<TripViewModel> Trips { get; }

        public TripsViewModel(TripService tripService)
        {
            this.tripService = tripService;
            Trips = new ObservableCollection<TripViewModel>();
        }

        public async Task LoadTrips()
        {
            if (Trips.Any())
            {
                Trips.Clear();
            }

            var trips = await tripService.GetTrips();
            
            foreach (var trip in trips)
            {
                Trips.Add(new TripViewModel(trip, tripService));
            }
        }
    }
}
