using domain.StockManager.Domain.Entities;
using infrastructure.StockManager.Infrastructure.Persistence;

namespace StockManager.Seeds
{
    public static class ColorTableSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Color.Any())
            {
                var colorsList = new List<Color>
                {
                    new Color("Vermelho"),
                    new Color("Verde"),
                    new Color("Azul"),
                };

                context.Color.AddRange(colorsList);
                context.SaveChanges();
            }
        }
    }
}