using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.DTOs;
using Travely.Domain.Entities;

namespace Travely.BusinessLogic.Converters
{
    public static class DTOEntity
    {
        public static TripSqlView DTOtoEntity(TripDTO trip)
        {
            return new TripSqlView
            {
                Title = trip.Title,
                Country = trip.Country,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
            };
        }

        public static PackingItemSqlView DTOtoEntity(PackingItemDTO packingItem)
        {
            return new PackingItemSqlView
            {
                Title = packingItem.Title,
                IsPacked = packingItem.IsPacked,
            };
        }

        public static FlightSqlView DTOtoEntity(FlightDTO flight)
        {
            return new FlightSqlView
            {
                Id = flight.Id,
                Origin = flight.Origin,
                Destination = flight.Destination,
                Status = flight.Status,
                FlightType = flight.FlightType,
            };
        }
    }
}
