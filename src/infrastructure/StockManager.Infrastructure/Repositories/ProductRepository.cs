using application.StockManager.Application.Interfaces;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task CreateProduct(Product product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação não pode ser concluida", ex);
            }
        }

        public async Task DeleteProduct(Product product)
        {
            try
            {
                _context.Product.RemoveRange(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de deletar não pode ser concluida", ex);
            }
        }

        public Task<IEnumerable<Product>> GetProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(Guid idProduct)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(p => p.Id == idProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de buscar por id não pode ser concluida", ex);
            }
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}