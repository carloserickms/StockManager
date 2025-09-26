using System.Reflection.Metadata;

namespace domain.StockManager.Domain.Entities
{
    public class Material : BaseModelAudit
    {
        public string Name { get; private set; }
        public double Amount { get; private set; }
        public decimal Value { get; private set; }

        public ICollection<Product>? Products { get; private set; } = new HashSet<Product>();
        public ICollection<Color>? Colors { get; private set; } = new HashSet<Color>();


        public Material() : base() { }

        public Material(string name, double amount, decimal value) : base()
        {
            Name = name;
            Amount = amount;
            Value = value;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("campo 'nome' não pode ser vazio");
            }

            Name = name;
            SetUpdated();
        }

        public void ReduceAmount(double amount)
        {
            if (!CheckAmoutInputAmout(amount))
            {
                throw new ArgumentException($"Não é possível informar valores negativos.");
            }

            Amount -= amount;
            SetUpdated();
        }

        public bool CheckAmoutInputAmout(double amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            return true;
        }

        public bool CheckAvailableQuantity(double amountMaterial, double amountProduct)
        {
            if (amountProduct <= 0)
                throw new ArgumentException("O consumo de material por produto deve ser maior que zero.");
            if (amountProduct < 0)
                throw new ArgumentException("Quantidade de produtos não pode ser negativa.");

            double totalMaterial = amountMaterial * amountProduct;

            if (Amount < totalMaterial)
            {
                return false;
            }

            return true;
        }

        public void UpdateValue(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Não é possível informar valores negativos.");
            }

            Value = value;
            SetUpdated();
        }

        public void AddInColorList(Color color)
        {
            Colors?.Add(color);
        }
    }
}