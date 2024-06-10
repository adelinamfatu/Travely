using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.Domain.CRUD;
using Travely.Domain;

namespace Travely.BusinessLogic.Services
{
    public class StatisticService
    {
        public StatisticData statisticData;

        public StatisticService(AppDbContext context)
        {
            this.statisticData = new StatisticData(context);
        }

        public Dictionary<string, int> GetTripCountries()
        {
            return statisticData.GetTripCountries();
        }
    }
}
