using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProduct();
        Task<Product> GetProductById(Guid idProduct);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}