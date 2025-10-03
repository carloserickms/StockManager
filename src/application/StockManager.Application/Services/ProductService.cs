using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;
using shared.StockManager.Shered.Utils;

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


        public async Task<Result<Product>> CreateProduct(CreateProductDto product, List<MaterialDto>? materials, List<int>? colorIds)
        {
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

            var colorsList = await _colorRepository.GetAllColorsOnTheList(colorIds);

            if (colorsList == null || !colorsList.Any())
            {
                return Result<Product>.Failure("Não foi possivel encontrar as cores inseridas.");
            }

            List<MaterialRequirement> materialRequirements = new();

            foreach (var materialListItem in materials)
            {
                var material = new MaterialRequirement(materialListItem.id, materialListItem.amount);
                materialRequirements.Add(material);
            }

            var productInfo = new ProductInfo(product.name, product.value, product.amount, product.urlImage, product.discount);

            try
            {
                var newProduct = Product.Create(productInfo, materialList, colorsList, materialRequirements);

                foreach (var material in materialList)
                {                    
                    await _materialRepository.UpdateMaterial(material);
                }

                await _productRepository.Save(newProduct);
            }
            catch (BusinessException ex)
            {
                return Result<Product>.Failure(ex.Message);
            }

            return Result<Product>.Created("Produto criado com sucesso.");
        }

        public Task<Result<Product>> DeleteProduct(ActionUserDto actionUser)
        {
            throw new NotImplementedException();
        }
    }
}