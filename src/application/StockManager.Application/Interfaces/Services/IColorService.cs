using application.StockManager.Application.Dtos.responses;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Service
{
    public interface IColorService
    {
        public Task<Result<IEnumerable<ColorResponseDto>>> GetAllColors();
    }
}