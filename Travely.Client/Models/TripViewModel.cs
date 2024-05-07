using System.Windows.Input;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class TripViewModel
    {
        private readonly TripService? tripService;

        public ICommand DeleteCommand { get; private set; }

        public Guid Id { get; set; }

        public string? TripTitle { get; set; }

        public string? CountryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? CountryURL { get; set; }

        public TripViewModel(TripDTO trip, TripService tripService)
        {
            this.Id = trip.Id;
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate; 
            this.EndDate = trip.EndDate;
            this.TripTitle = trip.Title;
            this.tripService = tripService;
            DeleteCommand = new Command<Guid>(ExecuteDeleteCommand);
        }

        public TripViewModel(TripService tripService) 
        { 
            this.tripService = tripService;
            DeleteCommand = new Command<Guid>(ExecuteDeleteCommand);
        }

        public void AddTrip()
        {
            if (!string.IsNullOrEmpty(TripTitle) && 
                !string.IsNullOrEmpty(CountryName) && 
                StartDate != default && 
                EndDate != default && 
                tripService is not null)
            {
                tripService.AddTrip(new TripDTO
                {
                    Title = TripTitle,
                    Country = CountryName,
                    StartDate = StartDate,
                    EndDate = EndDate
                });
            }
            else
            {
                
            }
        }

        private void ExecuteDeleteCommand(Guid tripId)
        {
            if (tripService is not null)
            {
                tripService.DeleteTrip(tripId);
            }
        }
    }
}
