using shared.StockManager.Shered;

namespace domain.StockManager.Domain.Entities
{
    public class Product : BaseModelAudit
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public double Amount { get; private set; }
        public string UrlImage { get; private set; }
        public double Discount { get; private set; }


        public Product() : base() { }

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
                throw new ArgumentException("Não é possível informar valores negativos.");
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
    }
}