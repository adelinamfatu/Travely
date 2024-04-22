using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadTrips();
        }

        private void LoadTrips()
        {
            var trips = tripService.GetTrips();
            Trips.Clear();
            foreach (var trip in trips)
            {
                Trips.Add(new TripViewModel(trip));
            }
        }
    }
}
