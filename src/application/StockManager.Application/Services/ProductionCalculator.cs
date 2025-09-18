using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;

namespace application.StockManager.Application.Service
{
    public static class ProductionCalculator
    {

        public static double CheckAvailableQuantity(Product product, IEnumerable<Material> materials, List<MaterialDto> materialRequired)
        {

            double maximumProductionProducts = product.Amount;

            foreach (var materialsItem in materials)
            {
                foreach (var materialRequiredItem in materialRequired)
                {
                    if (materialsItem.Id == materialRequiredItem.id)
                    {
                        double maxProdruct = materialsItem.GetMaxProductionByMaterial(materialRequiredItem.amount, product.Amount);

                        if (maxProdruct < maximumProductionProducts)
                        {
                            maximumProductionProducts = maxProdruct;
                        }
                    }
                }
            }

            return maximumProductionProducts;
        }
    }
}