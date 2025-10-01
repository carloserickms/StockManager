namespace domain.StockManager.Domain.Entities.ValueObjects
{
        public record MaterialRequirement(int MaterialId, double QuantityPerProduct);
        public record ProductInfo(string Name, decimal Value, double Amount, string UrlImage, double Discount);
}