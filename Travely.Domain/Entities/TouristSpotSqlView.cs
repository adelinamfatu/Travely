using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("TouristSpots")]
    public class TouristSpotSqlView
    {
        [Key]
        public Guid SpotId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        public decimal? DistanceFromCenter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EntryFee { get; set; } = 0;

        public virtual ICollection<BudgetTouristSpotSqlView> BudgetTouristSpots { get; set; } = new List<BudgetTouristSpotSqlView>();
    }
}
