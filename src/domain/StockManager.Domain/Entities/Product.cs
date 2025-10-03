using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using application.StockManager.Application.Service;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;
using shared.StockManager.Shered;

namespace domain.StockManager.Domain.Entities
{
    public class Product : BaseModelAudit
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public double Amount { get; private set; }
        public string? UrlImage { get; private set; }
        public double Discount { get; private set; }

        public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new List<ServiceOrder>();
        public ICollection<Material> Materials { get; private set; } = new HashSet<Material>();
        public ICollection<Color>? Colors { get; private set; } = new HashSet<Color>();
        
        private Product () : base () {}
        private Product(string name, double value, double amount, string urlImage, double discount) : base()
        {
            Name = name;
            Value = ConvertValueToInteger(value);
            Amount = amount;
            UrlImage = urlImage;
            Discount = discount;
        }

        public static Product Create(ProductInfo productInfo, IEnumerable<Material> materials, IEnumerable<Color> colorsList, IEnumerable<MaterialRequirement> requirements)
        {
            Product product = new(productInfo.Name, productInfo.Value, productInfo.Amount, productInfo.UrlImage, productInfo.Discount);

            if (!product.IsValidImageUrl(product.UrlImage))
            {
                throw new BusinessException("Url inválida");
            }

            bool availableQuantity = QuantityCalculator.CheckAvailableQuantityMaterials(product, materials, requirements);

            if (!availableQuantity)
            {
                throw new BusinessException("Não há material suficiente para criar os produtos");
            }

            foreach (var materialItem in materials)
            {
                foreach (var requiredItem in requirements)
                {
                    if (materialItem.Id == requiredItem.MaterialId)
                    {
                        materialItem.ReduceAmount(requiredItem.QuantityPerProduct * product.Amount);

                        product.AddInMaterialList(materialItem);
                    }
                }
            }

            foreach (var colorItem in colorsList)
            {
                product.AddInColorList(colorItem);
            }

            return product;
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

        public int ConvertValueToInteger(double value)
        {
            if (value == 0)
            {
                throw new BusinessException("Não foi possivel fazer a conversão. O campo valor esta como: 0");
            }

            int convertedValue = (int)(value * 100);

            return convertedValue;
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

        public void ReduceAmount(int amount)
        {
            if (!CheckAmoutInputAmout(amount))
            {
                throw new ArgumentException($"Não é possível informar valores negativos.");
            }

            Amount -= amount;
            SetUpdated();
        }

        public bool CheckAmoutInputAmout(int amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            return true;
        }

        public bool CheckAvailableQuantity(int amountProductRequered)
        {
            if (amountProductRequered <= 0)
            {
                throw new ArgumentException("Quantidade de produtos não pode ser menor ou igual a 0.");
            }

            if (Amount < amountProductRequered)
            {
                return false;
            }

            return true;
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