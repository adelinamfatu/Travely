using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Budgets")]
    public class BudgetSqlView
    {
        [Key]
        public Guid BudgetId { get; set; } = Guid.NewGuid();

        [ForeignKey("UserSqlView")]
        public int UserId { get; set; }
        public virtual UserSqlView User { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
        public string Details { get; set; }
    }
}
