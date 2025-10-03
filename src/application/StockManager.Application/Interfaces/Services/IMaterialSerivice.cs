using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces.Services
{
    public interface IMaterialService
    {
        public Task<Result<Material>> CreateMaterial(Material product, List<int>? colorIds);
        public Task<Result<Material>> DeleteMaterial(ActionUserDto actionUser);
        public Task<Result<IEnumerable<Material>>> GetAllMaterial();
    }
}