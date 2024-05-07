using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.DTOs;
using Travely.Domain.Entities;

namespace Travely.BusinessLogic.Converters
{
    public static class EntityDTO
    {
        public static TripDTO EntityToDTO(TripSqlView trip)
        {
            return new TripDTO
            {
                Id = trip.Id,
                Title = trip.Title,
                Country = trip.Country,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
            };
        }
    }
}
