using application.StockManager.Application.Service;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<ServiceOrder>
    {
    }
}