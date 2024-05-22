using Newtonsoft.Json;
using Travely.BusinessLogic.Converters;
using Travely.BusinessLogic.DTOs;
using Travely.Domain;
using Travely.Domain.CRUD;
using Travely.BusinessLogic.Resources;
using static Travely.BusinessLogic.Utilities.UtilitaryClasses;
using Travely.Domain.Entities;

namespace Travely.BusinessLogic.Services
{
    public class TripService
    {
        public TripData tripData;

        public TripService(AppDbContext context)
        {
            this.tripData = new TripData(context);
        }

        public void AddTrip(TripDTO trip)
        {
            tripData.AddTrip(DTOEntity.DTOtoEntity(trip));
        }

        public void DeleteTrip(Guid tripId)
        {
            tripData.DeleteTrip(tripId);
        }

        public void UpdateTripNotes(Guid tripId, string notes)
        {
            tripData.UpdateTripNotes(tripId, notes);
        }

        public async Task<List<TripDTO>> GetTrips()
        {
            var trips = await tripData.GetTrips();
            return trips.Select(trip => EntityDTO.EntityToDTO(trip)).ToList();
        }

        public async Task<TripDTO> GetTrip(Guid tripId)
        {
            var trip = await tripData.GetTrip(tripId) ?? new TripSqlView();
            return EntityDTO.EntityToDTO(trip);
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

        public async Task<List<string>> GetWorldCountries(List<string> continents)
        {
            var countries = new List<string>();
            using var httpClient = new HttpClient();

            foreach (var continent in continents)
            {
                var response = await httpClient.GetAsync(APICallResources.CountriesAPI + continent.Replace(" ", "%20"));

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

                    if (apiResponse is not null)
                    {
                        var data = apiResponse.Data;

                        if (data is not null)
                        {
                            foreach (var kvp in data)
                            {
                                if (kvp.Value.Country is not null)
                                {
                                    countries.Add(kvp.Value.Country);
                                }
                            }
                        }
                    }
                }
            }

            return countries;
        }
    }
}
