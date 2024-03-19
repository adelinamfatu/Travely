using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Flights")]
    public class FlightSqlView
    {
        [Key]
        public Guid FlightId { get; set; } = Guid.NewGuid();

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual ICollection<BudgetFlightSqlView> BudgetFlights { get; set; } = new List<BudgetFlightSqlView>();

    }
}
