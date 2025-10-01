using application.StockManager.Application.Interfaces;
using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Repository
{
    public class ColorRepository : RepositoryBase, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }

        public Task SaveColor(Color product)
        {
            throw new NotImplementedException();
        }

        public Task<Color> DeleteColor(Color product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Color>> GetColor()
        {
            try
            {
                return await _context.Color.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Operação não pode ser concluida", ex);
            }
        }

        public async Task<IEnumerable<Color>> GetAllColorsOnTheList(List<int> colorsList)
        {
            try
            {
                if (colorsList == null || colorsList.Count == 0)
                {
                    return Enumerable.Empty<Color>();
                }

                var colors = await _context.Color
                    .AsNoTracking()
                    .Where(c => colorsList.Contains(c.Id))
                    .ToListAsync();

                return colors;
            }
            catch (Exception ex)
            {
                throw new Exception("Operação não pode ser concluida", ex);
            }
        }

        public async Task<Color> GetColorById(int idProduct)
        {
            try
            {
                return await _context.Color.FirstAsync(c => c.Id == idProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("Operação não pode ser concluida", ex);
            }
        }

        public Task<Color> UpdateColor(Color product)
        {
            throw new NotImplementedException();
        }
    }
}