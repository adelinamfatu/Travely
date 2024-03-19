using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("BudgetTouristSpots")]
    public class BudgetTouristSpotSqlView
    {
        [Key, Column(Order = 0)]
        [ForeignKey("BudgetSqlView")]
        public Guid BudgetId { get; set; }
        public virtual BudgetSqlView Budget { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("TouristSpotSqlView")]
        public Guid SpotId { get; set; }
        public virtual TouristSpotSqlView TouristSpot { get; set; }
    }
}
