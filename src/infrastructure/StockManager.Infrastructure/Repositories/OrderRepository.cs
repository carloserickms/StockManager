using application.StockManager.Application.Interfaces.Repositories;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;

namespace infrastructure.StockManager.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public Task Delete(ServiceOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceOrder>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceOrder> GetById(int entity)
        {
            throw new NotImplementedException();
        }

        public async Task Save(ServiceOrder order)
        {
            try
            {
                foreach (var item in order.Product)
                {
                    _context.Attach(item);
                }

                await _context.ServiceOrder.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}