using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IProductRepository
    {
        Task SaveProduct(Product product);
        Task<IEnumerable<Product>> GetProduct();
        Task<Product> GetProductById(int idProduct);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}