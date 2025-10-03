
using System.ComponentModel.DataAnnotations;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Dtos.resquests
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime deliveryDay { get; set; }
        [Required]
        public bool isDelivery { get; set; }
        [Required]
        public string deliveryLocation { get; set; }
        [Required]
        public int paymentMethodId { get; set; }
        [Required]
        public int statusId { get; set; }
        [Required]
        public int customerId { get; set; }
        [Required]
        public List<ProductDto> products { get; set; }
    }
}