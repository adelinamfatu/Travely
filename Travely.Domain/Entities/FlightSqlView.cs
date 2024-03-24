using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Flights")]
    public class FlightSqlView
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? Origin { get; set; }

        [Required]
        public string? Destination { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("TripSqlView")]
        public Guid TripId { get; set; }

        public virtual TripSqlView? Trip { get; set; }
    }
}
