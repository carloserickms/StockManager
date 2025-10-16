using application.StockManager.Application.Dtos;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllMaterialsOnTheList(List<ProductDto> ProductList);
        Task Update(Product product);
    }
}