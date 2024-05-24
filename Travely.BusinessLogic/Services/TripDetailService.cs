using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.Domain.CRUD;
using Travely.Domain;
using Newtonsoft.Json;
using static Travely.BusinessLogic.Utilities.UtilitaryClasses;
using Travely.BusinessLogic.Resources;

namespace Travely.BusinessLogic.Services
{
    public class TripDetailService
    {
        public TripData tripData;

        public TripDetailService(AppDbContext context)
        {
            this.tripData = new TripData(context);
        }

        public async Task<List<DateTime>> GetTripDays(Guid tripId)
        {
            var tripDates = await tripData.GetTripDays(tripId);

            if (tripDates.Count < 2)
            {
                return tripDates;
            }

            var startDate = tripDates[0];
            var endDate = tripDates[1];

            var tripDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            return tripDays;
        }
    }
}
