using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IProductService
    {
        public Task<ResultResponseBase> CreateProduct(Product product, List<MaterialDto>? materialIds, List<Guid>? colorIds);
        public Task<ResultResponseBase> DeleteProduct(ActionUserDto actionUser);
    }
}