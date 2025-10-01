using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Result<Product>> CreateProduct(CreateProductDto product, List<MaterialDto>? materialIds, List<int>? colorIds);
        public Task<Result<Product>> DeleteProduct(ActionUserDto actionUser);
    }
}