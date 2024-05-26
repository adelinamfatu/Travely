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

        public async Task<string?> GetTripCountry(Guid tripId)
        {
            return await tripData.GetTripCountry(tripId);
        }

        public async Task<List<string>> GetCountryCoordinates(string country)
        {
            var coordinates = new List<string>();
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(string.Format(APICallResources.GeocodeCountryAPI, country));
            
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeAnonymousType(jsonString, new[] {
                    new {
                            lat = string.Empty,
                            lon = string.Empty
                        }
                    });

                if (apiResponse is not null && apiResponse.Length > 0)
                {
                    var firstResult = apiResponse[0];
                    coordinates.Add(firstResult.lat);
                    coordinates.Add(firstResult.lon);
                }
            }

            return coordinates;
        }

        public async Task<string> GetSpotName(double latitude, double longitude)
        {
            var spotName = string.Empty;
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(string.Format(APICallResources.GeocodeCoordinatesAPI, latitude, longitude));

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeAnonymousType(jsonString, new[] {
                    new {
                            lat = string.Empty,
                            lon = string.Empty
                        }
                    });

                if (apiResponse is not null && apiResponse.Length > 0)
                {
                    
                }
            }

            return spotName;
        }
    }
}
