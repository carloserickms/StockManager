using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;

namespace application.StockManager.Application.Service
{
    public static class ProductionCalculator
    {

        public static bool CheckAvailableQuantity(Product product, IEnumerable<Material> materials, IEnumerable<MaterialRequirement> materialRequired)
        {

            foreach (var materialsItem in materials)
            {
                foreach (var materialRequiredItem in materialRequired)
                {
                    if (materialsItem.Id == materialRequiredItem.MaterialId)
                    {
                        if (materialRequiredItem.QuantityPerProduct <= 0)
                        {
                            return false;
                        }

                        if (!materialsItem.CheckAvailableQuantity(materialRequiredItem.QuantityPerProduct, product.Amount))
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