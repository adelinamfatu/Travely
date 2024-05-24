namespace Travely.BusinessLogic.Utilities
{
    public class UtilitaryClasses
    {
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

        public class FlightInfo
        {
            public string? OriginAirport { get; set; }
            public string? DestinationAirport { get; set; }
            public string? FlightStatus { get; set; }
        }
    }
}
