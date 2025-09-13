using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Dtos.resquests
{
    public class CreateProductDto
    {
        public string name { get; set; }
        public decimal value { get; set; }
        public double amount { get; set; }
        public string? urlImage { get; set; }
        public double discount { get; set; }
        public List<MaterialDto> Materials { get; set; }
        public List<Guid>? ColorIds { get; set; }

        public Product ToProduct()
        {
            return new Product(name, value, amount, urlImage!, discount);
        }
    }
}