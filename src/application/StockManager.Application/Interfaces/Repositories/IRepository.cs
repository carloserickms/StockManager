using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task Save(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int entity);
        Task Delete(T entity);
    }
}