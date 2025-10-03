using application.StockManager.Application.Service;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;

namespace domain.StockManager.Domain.Entities
{
    public class ServiceOrder : BaseModelAudit
    {
        public string OrderNumber { get; private set; }
        public DateTime DeliveryDay { get; private set; }
        public bool IsDelivery { get; private set; }
        public string DeliveryLocation { get; private set; }
        public int OrderValue { get; private set; }
        public int PaymentMethodId { get; private set; }
        public int StatusId { get; private set; }
        public int CustomerId { get; private set; }

        public Customer Customer { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public Status Status { get; private set; }
        public ICollection<Product> Product { get; private set; } = new List<Product>();


        private ServiceOrder(DateTime deliveryDay, bool isDelivery, string deliveryLocation, int paymentMethodId, int statusId, int customerId) : base()
        {
            OrderNumber = $"AG#{DateTime.Now:yyMMddHHmmss}";;
            DeliveryDay = deliveryDay;
            IsDelivery = isDelivery;
            DeliveryLocation = deliveryLocation;
            PaymentMethodId = paymentMethodId;
            StatusId = statusId;
            CustomerId = customerId;
        }

        public static ServiceOrder Create(ServiceOrderInfo serviceOrderInfo, IEnumerable<Product> products, IEnumerable<ProductsRequirement> productsRequirements)
        {
            ServiceOrder serviceOrder = new(serviceOrderInfo.DeliveryDay, serviceOrderInfo.IsDelivery, serviceOrderInfo.DeliveryLocation, serviceOrderInfo.PaymentMethodId, serviceOrderInfo.StatusId, serviceOrderInfo.CustomerId);

            if (products == null || !products.Any())
            {
                throw new BusinessException("Você precisa associar produtos que compoem a ordem de serviço.");
            }

            bool availableQuantity = QuantityCalculator.CheckAvailableQuantityProduct(products, productsRequirements);

            if (!availableQuantity)
            {
                throw new BusinessException("Não há Produtos suficiente para adicionar");
            }

            foreach (var productItem in products)
            {
                foreach (var requiredItem in productsRequirements)
                {
                    if (productItem.Id == requiredItem.ProductId)
                    {
                        productItem.ReduceAmount(requiredItem.QuantityPerProduct);

                        serviceOrder.Product.Add(productItem);
                    }
                }
            }

            serviceOrder.OrderValue = serviceOrder.CalculateCotalValueOfServiceOrder(products, productsRequirements);

            return serviceOrder;
        }

        public int CalculateCotalValueOfServiceOrder(IEnumerable<Product> products, IEnumerable<ProductsRequirement> productsRequirements)
        {
            int total = 0;

            foreach (var productItem in products)
            {
                foreach (var requiredItem in productsRequirements)
                {
                    if (productItem.Id == requiredItem.ProductId)
                    {
                        total = productItem.Value * requiredItem.QuantityPerProduct;
                    }
                }
            }

            return total;
        }
    }
}