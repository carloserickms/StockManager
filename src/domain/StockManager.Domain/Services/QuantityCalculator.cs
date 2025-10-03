using System.Reflection.Metadata;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;

namespace application.StockManager.Application.Service
{
    public static class QuantityCalculator
    {

        public static bool CheckAvailableQuantityMaterials(Product product, IEnumerable<Material> materials, IEnumerable<MaterialRequirement> materialRequired)
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

        public static bool CheckAvailableQuantityProduct(IEnumerable<Product> products, IEnumerable<ProductsRequirement> productsRequirements)
        {
            foreach (var productItem in products)
            {
                foreach (var requiredItem in productsRequirements)
                {
                    if (productItem.Id == requiredItem.ProductId)
                    {
                        if (requiredItem.QuantityPerProduct == 0)
                        {
                            return false;
                        }

                        if (!productItem.CheckAvailableQuantity(requiredItem.QuantityPerProduct))
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