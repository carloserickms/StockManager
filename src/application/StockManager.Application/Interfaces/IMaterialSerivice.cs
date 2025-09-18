using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IMaterialSerivice
    {
        public Task<ResultResponseBase> CreateMaterial(Material product, List<int>? colorIds);
        public Task<ResultResponseBase> DeleteMaterial(ActionUserDto actionUser);
    }
}