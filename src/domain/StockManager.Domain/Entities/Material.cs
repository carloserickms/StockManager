using System.Reflection.Metadata;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;

namespace domain.StockManager.Domain.Entities
{
    public class Material : BaseModelAudit
    {
        public string Name { get; private set; }
        public double Amount { get; private set; }
        public decimal Value { get; private set; }

        public ICollection<Product>? Products { get; private set; } = new HashSet<Product>();
        public ICollection<Color>? Colors { get; private set; } = new HashSet<Color>();


        public Material(string name, double amount, decimal value) : base()
        {
            Name = name;
            Amount = amount;
            Value = value;
        }

        public static Material Create(MaterialInfo materialInfo, IEnumerable<Color> colors)
        {
            Material material = new(materialInfo.Name, materialInfo.Amount, materialInfo.Value);

            if (!material.CheckAmoutInputAmout(materialInfo.Amount))
            {
                throw new BusinessException("A quantidade não pode ser menor ou igual a 0.");
            }

            if (colors != null || !colors.Any())
            {
                foreach (var item in colors)
                {
                    material.AddInColorList(item);
                }
            }

            return material;
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