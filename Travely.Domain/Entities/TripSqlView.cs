using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Trips")]
    public class TripSqlView
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<SpotSqlView>? Spots { get; set; }

        public virtual ICollection<FlightSqlView>? Flights { get; set; }
    }
}
