using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Dtos.resquests
{
    public class CreateMaterialDto
    {
        public string name { get; set; }
        public double amount { get; set; }
        public decimal value { get; set; }
        public List<Guid>? ColorIds { get; set; }


        public Material ToMaterial()
        {
            return new Material(name, amount, value);
        }
    }
}