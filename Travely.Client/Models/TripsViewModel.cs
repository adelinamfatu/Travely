using System.Collections.ObjectModel;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class TripsViewModel
    {
        private readonly TripService tripService;
        public ObservableCollection<TripViewModel> Trips { get; }
        private List<TripViewModel> allTrips;
        private bool showPastTrips;

        public TripsViewModel(TripService tripService)
        {
            this.tripService = tripService;
            Trips = new ObservableCollection<TripViewModel>();
            allTrips = new List<TripViewModel>();
            showPastTrips = false;
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
            var today = DateTime.Today;

            var filteredTrips = showPastTrips
                ? allTrips
                : allTrips.Where(t => t.StartDate >= today).ToList();

            var orderedTrips = showPastTrips
                ? filteredTrips.OrderBy(t => t.StartDate).ToList()
                : filteredTrips.OrderByDescending(t => t.StartDate).ToList();

            foreach (var trip in orderedTrips)
            {
                Trips.Add(trip);
            }
        }

        public void TogglePastTrips()
        {
            showPastTrips = !showPastTrips;
            UpdateTripsList();
        }
    }
}
