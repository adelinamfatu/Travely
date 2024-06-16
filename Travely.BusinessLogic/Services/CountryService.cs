using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Travely.BusinessLogic.Resources;

namespace Travely.BusinessLogic.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<JArray> GetAllCountries()
        {
            var url = APICallResources.CountryCurrencyAPI;
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            var countries = json["data"].ToObject<JArray>();

            var euroCountries = new JArray();

            foreach (var country in countries)
            {
                if (country["currency"].ToString().ToLower() == "eur")
                {
                    euroCountries.Add(country);
                }
            }

            return euroCountries;
        }

        public async Task<JArray> GetCountriesWithLatLong()
        {
            var url = APICallResources.CountryLatitudeAPI;
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            var countries = json["data"].ToObject<JArray>();
            return countries;
        }

        public async Task<JArray> GetFilteredCountriesByLatLong()
        {
            var countries = await GetCountriesWithLatLong();
            var filteredCountries = new JArray();

            foreach (var country in countries)
            {
                double lat = (double)country["lat"];
                if (lat >= 35 && lat <= 60)
                {
                    filteredCountries.Add(country);
                }
            }

            return filteredCountries;
        }

        public JArray GetPopularBeachDestinations()
        {
            var beachDestinations = new JArray
            {
                new JObject { { "name", "Santorini, Greece" } },
                new JObject { { "name", "Ibiza, Spain" } },
                new JObject { { "name", "Amalfi Coast, Italy" } },
                new JObject { { "name", "Nice, France" } },
                new JObject { { "name", "Mykonos, Greece" } },
                new JObject { { "name", "Sardinia, Italy" } },
                new JObject { { "name", "Côte d'Azur, France" } },
                new JObject { { "name", "Mallorca, Spain" } },
                new JObject { { "name", "Dubrovnik, Croatia" } },
                new JObject { { "name", "Malaga, Spain" } },
                new JObject { { "name", "Madeira, Portugal" } },
                new JObject { { "name", "Crete, Greece" } },
                new JObject { { "name", "Lisbon Coast, Portugal" } },
                new JObject { { "name", "Paphos, Cyprus" } },
                new JObject { { "name", "Tenerife, Spain" } }
            };

            return beachDestinations;
        }
    }
}
