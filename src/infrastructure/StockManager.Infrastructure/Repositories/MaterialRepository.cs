using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace infrastructure.StockManager.Infrastructure.Repository
{
    public class MaterialRepository : RepositoryBase, IMaterialRepository
    {
        public MaterialRepository(AppDbContext context) : base(context)
        {
        }

        public async Task SaveMaterial(Material material)
        {

            foreach (var item in material.Colors)
            {
                _context.Attach(item);
            }

            await _context.Material.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public Task DeleteMaterial(Material material)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsOnTheList(List<MaterialDto> materialList)
        {
            try
            {
                if (materialList == null || materialList.Count == 0)
                {
                    return Enumerable.Empty<Material>();
                }

                var ids = materialList
                    .Select(m => m.id)
                    .Where(id => id != default)
                    .Distinct()
                    .ToList();

                if (ids.Count == 0)
                {
                    return Enumerable.Empty<Material>();
                }

                var materials = await _context.Material
                    .AsNoTracking()
                    .Where(m => ids.Contains(m.Id))
                    .ToListAsync();

                return materials;
            }
            catch (Exception ex)
            {
                throw new Exception("Operação de buscar por id não pode ser concluida", ex);
            }
        }

        public Task<Material> GetMaterialById(int idMaterial)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Material>> GetMaterials()
        {
            return await _context.Material.ToListAsync();
        }

        public async Task UpdateMaterial(Material material)
        {
            _context.Material.Update(material);
            await _context.SaveChangesAsync();
        }
    }
}