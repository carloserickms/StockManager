using System.ComponentModel.DataAnnotations;

namespace domain.StockManager.Domain.Entities
{
    public abstract class BaseModelId
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }

        protected BaseModelId()
        {
            Id = Guid.NewGuid();
        }
    }
}