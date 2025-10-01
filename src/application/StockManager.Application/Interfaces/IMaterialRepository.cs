using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Interfaces
{
    public interface IMaterialRepository
    {
        Task SaveMaterial(Material product);
        Task<IEnumerable<Material>> GetMaterials();
        Task<IEnumerable<Material>> GetAllMaterialsOnTheList(List<MaterialDto> materialList);
        Task<Material> GetMaterialById(int idProduct);
        Task UpdateMaterial(Material product);
        Task DeleteMaterial(Material product);
    }
}