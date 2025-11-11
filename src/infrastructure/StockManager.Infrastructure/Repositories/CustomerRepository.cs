using application.StockManager.Application.Interfaces.Repositories;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public Task Delete(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _context.Customer.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de salvar não pode ser concluida", ex);
            }
        }

        public Task<Customer> GetById(int customer)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Customer customer)
        {
            try
            {
                await _context.Customer.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de salvar não pode ser concluida", ex);
            }
        }
    }
}