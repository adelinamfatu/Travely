using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("BudgetFlights")]
    public class BudgetFlightSqlView
    {
        [Key, Column(Order = 0)]
        [ForeignKey("BudgetSqlView")]
        public Guid BudgetId { get; set; }
        public virtual BudgetSqlView Budget { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("FlightSqlView")]
        public Guid FlightId { get; set; }
        public virtual FlightSqlView Flight { get; set; }
    }
}
