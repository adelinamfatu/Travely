using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.DTOs;

namespace Travely.Client.Models
{
    public class TripViewModel
    {
        public TripViewModel(TripDTO trip)
        {
            this.CountryName = trip.Country;
            this.StartDate = trip.StartDate; 
            this.EndDate = trip.EndDate;
        }

        public string? CountryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? CountryURL {  get; set; }
    }
}
