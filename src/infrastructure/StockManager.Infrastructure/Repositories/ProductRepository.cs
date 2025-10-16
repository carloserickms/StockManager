using application.StockManager.Application.Dtos;
using application.StockManager.Application.Interfaces.Repositories;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task Save(Product product)
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

        public async Task Delete(Product product)
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

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetById(int idProduct)
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

        public async Task Update(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllMaterialsOnTheList(List<ProductDto> ProductsList)
        {
            try
            {
                if (ProductsList == null || ProductsList.Count == 0)
                {
                    return Enumerable.Empty<Product>();
                }

                var ids = ProductsList
                    .Select(p => p.id)
                    .Where(id => id != default)
                    .Distinct()
                    .ToList();

                if (ids.Count == 0)
                {
                    return Enumerable.Empty<Product>();
                }

                var products = await _context.Product
                    .AsNoTracking()
                    .Where(p => ids.Contains(p.Id))
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de buscar por id não pode ser concluida", ex);
            }
        }
    }
}