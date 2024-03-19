using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travely.Domain.Entities
{
    [Table("Users")]
    public class UserSqlView
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public virtual ICollection<BudgetSqlView> Budgets { get; set; } = new List<BudgetSqlView>();

    }
}
