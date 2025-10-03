namespace domain.StockManager.Domain.Entities.ValueObjects
{
        public record MaterialRequirement(int MaterialId, double QuantityPerProduct);
        public record ProductsRequirement(int ProductId, int QuantityPerProduct);
        public record ProductInfo(string Name, double Value, double Amount, string UrlImage, double Discount);
        public record MaterialInfo(string Name, double Amount, decimal Value);
        public record ServiceOrderInfo(DateTime DeliveryDay, bool IsDelivery, string DeliveryLocation, int PaymentMethodId, int StatusId, int CustomerId);
}