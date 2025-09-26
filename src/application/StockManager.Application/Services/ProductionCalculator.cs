using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Service
{
    public static class ProductionCalculator
    {

        public static bool CheckAvailableQuantity(Product product, IEnumerable<Material> materials, List<MaterialDto> materialRequired)
        {

            foreach (var materialsItem in materials)
            {
                foreach (var materialRequiredItem in materialRequired)
                {
                    if (materialsItem.Id == materialRequiredItem.id)
                    {
                        if (materialRequiredItem.amount <= 0)
                        {
                            return false;
                        }

                        if (!materialsItem.CheckAvailableQuantity(materialRequiredItem.amount, product.Amount))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}