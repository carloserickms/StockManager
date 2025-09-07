namespace domain.StockManager.Domain.Entities
{
    public class Material : BaseModelAudit
    {
        public string Name { get; private set; }
        public double Amount { get; private set; }
        public decimal Value { get; private set; }

        public ICollection<Product>? Products { get; private set; } = new HashSet<Product>();


        public Material() : base() { }

        public Material(string name, double amount, decimal value) : base()
        {
            Name = name;
            Amount = amount;
            Value = value;
        }
    }
}