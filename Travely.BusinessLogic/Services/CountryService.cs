using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var url = "https://countriesnow.space/api/v0.1/countries/currency";
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

            
            Console.WriteLine($"Number of countries fetched: {countries.Count}");

            return euroCountries;
        }

    }
}
