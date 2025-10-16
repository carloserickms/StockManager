using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<Result<ServiceOrder>> CreateOrder(CreateOrderDto createOrder, List<ProductDto> products);
    }
}