using application.StockManager.Application.Dtos;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.responses
{
    public class ProductResponseDto : ResultResponseBase
    {
        public string name { get; set; }
        public decimal value { get; set; }
        public double amount { get; set; }
        public string? urlImage { get; set; }
        public double discount { get; set; }
        public ICollection<ColorResponseDto>? colors { get; set; }
        public override string message { get; set; }
    }
}