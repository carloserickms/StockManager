using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<IEnumerable<Color>> GetAllColorsOnTheList(List<int> colors);
    }
}