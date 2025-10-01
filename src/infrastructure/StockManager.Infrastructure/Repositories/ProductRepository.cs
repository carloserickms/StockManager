using application.StockManager.Application.Interfaces;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task SaveProduct(Product product)
        {
            try
            {
                foreach (var item in product.Materials)
                {
                    _context.Attach(item);
                }

                foreach (var item in product.Colors)
                {
                    _context.Attach(item);
                }

                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteProduct(Product product)
        {
            try
            {
                _context.Product.RemoveRange(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Product>> GetProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int idProduct)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(p => p.Id == idProduct);
            }
            catch
            {
                throw;
            }
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}