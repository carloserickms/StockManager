using System.ComponentModel.DataAnnotations;

namespace domain.StockManager.Domain.Entities
{
    public abstract class BaseModelAudit
    {

        [Key]
        [Required]
        public Guid Id { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        [Required]
        public DateTime UpdatedAt { get; private set; }

        protected BaseModelAudit()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void SetUpdated()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}