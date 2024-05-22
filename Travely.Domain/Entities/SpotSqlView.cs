using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Spots")]
    public class SpotSqlView
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Location { get; set; }

        public decimal? DistanceFromCenter { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public decimal? EntryFee { get; set; }

        [ForeignKey("TripSqlView")]
        public Guid TripId { get; set; }

        public virtual TripSqlView? Trip { get; set; }
    }
}
