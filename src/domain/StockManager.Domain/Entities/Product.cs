using System.Text.RegularExpressions;
using shared.StockManager.Shered;

namespace domain.StockManager.Domain.Entities
{
    public class Product : BaseModelAudit
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public double Amount { get; private set; }
        public string? UrlImage { get; private set; }
        public double Discount { get; private set; }

        public ICollection<Material> Materials { get; private set; } = new HashSet<Material>();
        public ICollection<Color>? Colors { get; private set; } = new HashSet<Color>();

        public Product(string name, decimal value, double amount, string urlImage, double discount) : base()
        {
            Name = name;
            Value = value;
            Amount = amount;
            UrlImage = urlImage;
            Discount = discount;
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

        public void UpdateUrlImage(string urlImage)
        {
            if (!RegexUtils.IsValidImageUrl(urlImage))
            {
                throw new ArgumentException("Url da imagem não é compativel com o formato, verifique se a url contém: https, .png ou .jpg.");
            }

            UrlImage = urlImage;
            SetUpdated();
        }

        public void UpdateAmount(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"Não é possível informar valores negativos. {amount}");
            }

            Amount = amount;
            SetUpdated();
        }

        public void ApplyDiscount(double discount)
        {
            if (discount < 0 || discount > 100)
            {
                throw new ArgumentException("Desconto deve ser entre 0% a 100%.");
            }

            Discount = discount;
            SetUpdated();
        }

        public void AddInMaterialList(Material material)
        {
            if (material != null)
            {
                Console.WriteLine(material.Name);
                Materials.Add(material);
            }
        }

        public void AddInColorList(Color color)
        {
            Colors?.Add(color);
        }

        public bool IsValidImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            // Regex: começa com https:// e termina com .jpg ou .png
            string pattern = @"^https:\/\/.+\.(jpg|png)$";

            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }
    }
}