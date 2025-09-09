using application.StockManager.Application.Interfaces;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;

namespace infrastructure.StockManager.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
  
        public async Task CreateProduct(Product product)
        {
            try
            {
                var result = await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação não pode ser concluida", ex);
            }
        }

        public Task<Product> DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProduct()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(Guid idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}