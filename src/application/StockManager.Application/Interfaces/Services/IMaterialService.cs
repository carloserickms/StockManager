using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces.Services
{
    public interface IMaterialService
    {
        public Task<Result<Material>> CreateMaterial(Material product, List<int>? colorIds);
        public Task<Result<Material>> DeleteMaterial(ActionUserDto actionUser);
        public Task<Result<MaterialResponseDto>> GetMaterialById(int id);
        public Task<Result<IEnumerable<MaterialResponseDto>>> GetAllMaterial(int page);
        public Task<Result<IEnumerable<MaterialResponseDto>>> GetMaterialByName(string name);
    }
}