
namespace domain.StockManager.Domain.Entities
{
    public class Color : BaseModelId
    {
        public string Name { get; private set; }

        public ICollection<Product>? Products { get; private set; } = new HashSet<Product>();
        public ICollection<Material>? Materials { get; private set; } = new HashSet<Material>();

        public Color() : base() { }

        public Color(string name) : base()
        {
            Name = name;
        }
    }
}