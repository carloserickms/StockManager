using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IColorRepository
    {
        Task CreateColor(Color product);
        Task<IEnumerable<Color>> GetColor();
        Task<Color> GetColorById(Guid idProduct);
        Task<Color> UpdateColor(Color product);
        Task<Color> DeleteColor(Color product);
    }
}