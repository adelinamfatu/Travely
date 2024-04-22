using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class TripViewModel
    {
        private readonly TripService? tripService;

        public string? CountryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? CountryURL { get; set; }

        public TripViewModel(TripDTO trip)
        {
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate; 
            this.EndDate = trip.EndDate;
        }

        public TripViewModel(TripService tripService) 
        { 
            this.tripService = tripService;
        }

        public void AddTrip()
        {
            if (!string.IsNullOrEmpty(CountryName) && StartDate != default && EndDate != default && tripService is not null)
            {
                tripService.AddTrip(new TripDTO
                {
                    Country = CountryName,
                    StartDate = StartDate,
                    EndDate = EndDate
                });
            }
            else
            {
                
            }
        }
    }
}
