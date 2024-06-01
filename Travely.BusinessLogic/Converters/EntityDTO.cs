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
                Notes = trip.Notes,
            };
        }

        public static PackingItemDTO EntityToDTO(PackingItemSqlView packingItem)
        {
            return new PackingItemDTO
            {
                Id = packingItem.Id,
                Title = packingItem.Title,
                IsPacked = packingItem.IsPacked,
            };
        }

        public static FlightDTO EntityToDTO(FlightSqlView flight)
        {
            return new FlightDTO
            {
                Id = flight.Id,
                Origin = flight.Origin,
                Destination = flight.Destination,
                FlightType = flight.FlightType,
                Status = flight.Status,
            };
        }
    }
}
