using Travely.Domain.Entities;

namespace Travely.BusinessLogic.DTOs
{
    public class FlightDTO
    {
        public Guid Id { get; set; }

        public string? Origin { get; set; }

        public string? Destination { get; set; }

        public string? Status { get; set; }

        public FlightType FlightType { get; set; }
    }
}
