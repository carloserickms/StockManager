using application.StockManager.Application.Interfaces.Repositories;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repositories
{
    public class GenderRepository : RepositoryBase, IGenderRepository
    {

        public GenderRepository (AppDbContext context ) : base(context)
        {
        }

        public Task Delete(Gender entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Gender>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Gender> GetById(int id)
        {
            try
            {
                return await _context.Gender.FirstAsync(g => g.Id == id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task Save(Gender entity)
        {
            throw new NotImplementedException();
        }
    }
}