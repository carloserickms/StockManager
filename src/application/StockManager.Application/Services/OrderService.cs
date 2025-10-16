using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<ServiceOrder>> CreateOrder(CreateOrderDto createOrder, List<ProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return Result<ServiceOrder>.Failure("Lista de produtos não pode esta vazia");
            }

            ServiceOrderInfo serviceOrderInfo = new(createOrder.deliveryDay, createOrder.isDelivery, createOrder.deliveryLocation, createOrder.paymentMethodId, createOrder.statusId, createOrder.customerId);

            List<ProductsRequirement> requirementsList = new List<ProductsRequirement>();

            foreach (var item in products)
            {
                ProductsRequirement requiredItem = new ProductsRequirement(item.id, item.amount);
                requirementsList.Add(requiredItem);
            }

            var productsList = await _productRepository.GetAllMaterialsOnTheList(products);

            var newOrder = ServiceOrder.Create(serviceOrderInfo, productsList, requirementsList);

            foreach (var item in newOrder.Product)
            {
                await _productRepository.Update(item);
            }

            await _orderRepository.Save(newOrder);

            return Result<ServiceOrder>.Created("Produto criado com sucesso.");
        }
    }
}