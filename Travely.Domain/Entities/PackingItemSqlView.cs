using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("PackingItems")]
    public class PackingItemSqlView
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? Title { get; set; }

        [Required]
        public bool IsPacked { get; set; }
    }
}
