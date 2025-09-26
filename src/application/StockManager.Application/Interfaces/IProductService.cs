using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.responses;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Result<Product>> CreateProduct(Product product, List<MaterialDto>? materialIds, List<int>? colorIds);
        public Task<Result<Product>> DeleteProduct(ActionUserDto actionUser);
    }
}