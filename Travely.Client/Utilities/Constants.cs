using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travely.Client.Utilities
{
    public class Constants
    {
        public static readonly List<string> Continents = new List<string>
        {
            "Africa",
            "Antarctic",
            "Asia",
            "Europe",
            "Oceania",
            "North America",
            "South America"
        };

        public static string DatabaseFileName = "travely.db";

        public static string CountriesCacheFileName = "countries.json";

        public static string CoordinatesCacheFileName = "coordinates.json";
    }
}
