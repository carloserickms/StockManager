using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IColorRepository
    {
        Task SaveColor(Color product);
        Task<IEnumerable<Color>> GetColor();
        Task<IEnumerable<Color>> GetAllColorsOnTheList(List<int> colors);
        Task<Color> GetColorById(int idProduct);
        Task<Color> UpdateColor(Color product);
        Task<Color> DeleteColor(Color product);
    }
}