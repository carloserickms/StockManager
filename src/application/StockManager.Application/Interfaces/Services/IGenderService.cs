using application.StockManager.Application.Dtos.responses;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces.Services
{
    public interface IGenderService
    {
        Task<Result<IEnumerable<GenderResponseDto>>> GetAllGender();
    }
}