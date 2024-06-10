using Travely.Domain.CRUD;
using Travely.Domain;
using Newtonsoft.Json;
using Travely.BusinessLogic.Resources;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Converters;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Travely.BusinessLogic.Services
{
    public class TripDetailService
    {
        public TripData tripData;

        public TripDetailService(AppDbContext context)
        {
            this.tripData = new TripData(context);
        }

        public void AddSpot(SpotDTO spot, Guid tripId)
        {
            tripData.AddSpot(DTOEntity.DTOtoEntity(spot), tripId);
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

        public async Task<List<SpotDTO>> GetTripSpots(Guid tripId)
        {
            var spots = await tripData.GetTripSpots(tripId);
            return spots.Select(spot => EntityDTO.EntityToDTO(spot)).ToList();
        }

        public void UpdateSpotFee(Guid spotId, decimal fee)
        {
            tripData.UpdateSpot(spotId, fee);
        }

        public void UpdateSpotTime(Guid spotId, DateTime time)
        {
            tripData.UpdateSpot(spotId, time);
        }

        public void DeleteSpot(Guid spotId)
        {
            tripData.DeleteSpot(spotId);
        }

        public async Task<string?> GetTripCountry(Guid tripId)
        {
            return await tripData.GetTripCountry(tripId);
        }

        public async Task<List<string>> GetPlaceCoordinates(string place)
        {
            var coordinates = new List<string>();
            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(string.Format(APICallResources.GeocodeCountryAPI, place));
            
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

        public async Task<string> GetSpotDetails(double latitude, double longitude)
        {
            var spotDetails = string.Empty;
            using var httpClient = new HttpClient();

            var encodedLatitude = Uri.EscapeDataString(latitude.ToString(CultureInfo.InvariantCulture));
            var encodedLongitude = Uri.EscapeDataString(longitude.ToString(CultureInfo.InvariantCulture));

            var url = string.Format(APICallResources.GeocodeCoordinatesAPI, encodedLatitude, encodedLongitude);
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeAnonymousType(jsonString, new
                {
                    display_name = string.Empty
                });

                if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.display_name))
                {
                    spotDetails = apiResponse.display_name;
                }
            }

            return spotDetails;
        }

        public async Task<WeatherDTO> GetCountryWeather(double latitude, double longitude)
        {
            var weatherInfo = new WeatherDTO();

            using var httpClient = new HttpClient();

            var encodedLatitude = Uri.EscapeDataString(latitude.ToString(CultureInfo.InvariantCulture));
            var encodedLongitude = Uri.EscapeDataString(longitude.ToString(CultureInfo.InvariantCulture));

            var url = string.Format(APICallResources.WeatherAPI, encodedLatitude, encodedLongitude);
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var weatherJson = JObject.Parse(jsonString);

                weatherInfo = new WeatherDTO
                {
                    Temperature = weatherJson["main"]?["temp"]?.Value<double>(),
                    WindSpeed = weatherJson["wind"]?["speed"]?.Value<double>(),
                    Humidity = weatherJson["main"]?["humidity"]?.Value<double>(),
                    Pressure = weatherJson["main"]?["pressure"]?.Value<double>()
                };
            }

            return weatherInfo;
        }
    }
}
