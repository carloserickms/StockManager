using application.StockManager.Application.Dtos;
using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
using application.StockManager.Application.responses;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered;

namespace application.StockManager.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMaterialRepository _materialRepository;

        public ProductService(IProductRepository productRepository, IColorRepository colorRepository, IMaterialRepository materialRepository)
        {
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _materialRepository = materialRepository;
        }


        public async Task<Result<Product>> CreateProduct(Product product, List<MaterialDto>? materials, List<int>? colorIds)
        {
            if (!product.IsValidImageUrl(product.UrlImage))
            {
                return Result<Product>.Failure("Url da imagem não é compatível com o formato, verifique se contém: https, .png ou .jpg.");
            }

            if (materials == null || !materials.Any())
            {
                return Result<Product>.Failure("Lista de materias esta vazia");
            }

            if (colorIds == null || !colorIds.Any())
            {
                return Result<Product>.Failure("Lista de cores está vazia");
            }

            var materialList = await _materialRepository.GetAllMaterialsOnTheList(materials);

            if (materialList == null || !materialList.Any())
            {
                return Result<Product>.Failure("Não foi possivel encontrar os materiais inseridos.");
            }

            var checkProductCreation = ProductionCalculator.CheckAvailableQuantity(product, materialList, materials);

            if (!checkProductCreation)
            {
                return Result<Product>.Failure($"Quantidade de materiais em estoque não é suficiente.");
            }

            foreach (var materialItem in materialList)
            {
                foreach (var materialRequiredItem in materials)
                {
                    if (materialItem.Id == materialRequiredItem.id)
                    {
                        materialItem.ReduceAmount(materialRequiredItem.amount * product.Amount);

                        product.AddInMaterialList(materialItem);

                        _productRepository.AttachMaterial(materialItem);
                        await _materialRepository.UpdateMaterial(materialItem);
                    }
                }
            }

            var colorList = await _colorRepository.GetAllColorsOnTheList(colorIds);

            if (colorList != null)
            {
                foreach (var colorItem in colorList)
                {
                    _productRepository.AttachColor(colorItem);
                    product.AddInColorList(colorItem);
                }
            }

            await _productRepository.CreateProduct(product);

            return Result<Product>.Created("Produto criado com sucesso.");
        }

        public Task<Result<Product>> DeleteProduct(ActionUserDto actionUser)
        {
            throw new NotImplementedException();
        }
    }
}