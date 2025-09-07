using infrastructure.StockManager.Infrastructure.Persistence;

namespace infrastructure.StockManager.Infrastructure.Repository
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