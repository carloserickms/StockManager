using infrastructure.StockManager.Infrastructure.Persistence;

namespace infrastructure.StockManager.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

    }
}