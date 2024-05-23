using System.Collections.ObjectModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class TripsViewModel
    {
        private readonly TripService tripService;
        public ObservableCollection<TripViewModel> Trips { get; }
        private List<TripViewModel> allTrips;
        private bool isReversed;

        public TripsViewModel(TripService tripService)
        {
            this.tripService = tripService;
            Trips = new ObservableCollection<TripViewModel>();
            allTrips = new List<TripViewModel>();
            isReversed = false;
        }

        public async Task LoadTrips()
        {
            if (Trips.Any())
            {
                Trips.Clear();
            }

            var trips = await tripService.GetTrips();
            allTrips = trips.Select(trip => new TripViewModel(trip, tripService)).ToList();

            UpdateTripsList();
        }

        private void UpdateTripsList()
        {
            Trips.Clear();

            var orderedTrips = allTrips.OrderByDescending(t => t.StartDate).ToList();

            if (isReversed)
            {
                orderedTrips.Reverse();
            }

            foreach (var trip in orderedTrips)
            {
                Trips.Add(trip);
            }
        }

        public void TogglePastTrips()
        {
            isReversed = !isReversed;
            UpdateTripsList();
        }
    }
}
