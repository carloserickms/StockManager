using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> GetAllMaterialsOnTheList(List<MaterialDto> materialList);
        Task UpdateMaterial(Material product);
    }
}