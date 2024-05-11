using Newtonsoft.Json;
using Travely.BusinessLogic.Converters;
using Travely.BusinessLogic.DTOs;
using Travely.Domain;
using Travely.Domain.CRUD;
using Travely.BusinessLogic.Resources;

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

        public async Task<List<TripDTO>> GetTrips()
        {
            var trips = await tripData.GetTrips();
            return trips.Select(trip => EntityDTO.EntityToDTO(trip)).ToList();
        }

        public void DeleteTrip(Guid tripId)
        {
            tripData.DeleteTrip(tripId);
        }

        public async Task<List<string>> GetWorldCountries(List<string> continents)
        {
            using var httpClient = new HttpClient();

            foreach (var continent in continents)
            {
                var response = await httpClient.GetAsync(APICallResources.CountriesAPI + continent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

                    if (apiResponse is not null)
                    {
                        var data = apiResponse.Data;

                        if (data is not null)
                        {
                            var countries = new List<string>();

                            foreach (var kvp in data)
                            {
                                if (kvp.Value.Country is not null)
                                {
                                    countries.Add(kvp.Value.Country);
                                }
                            }

                            return countries;
                        }
                    }
                }
            }

            return new List<string>();
        }

        public class CountryInfo
        {
            public string? Country { get; set; }
            public string? Region { get; set; }
        }

        public class ApiResponse
        {
            public string? Status { get; set; }
            public int StatusCode { get; set; }
            public string? Version { get; set; }
            public string? Access { get; set; }
            public Dictionary<string, CountryInfo>? Data { get; set; }
        }
    }
}
