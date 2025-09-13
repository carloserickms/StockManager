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


        public async Task<ResultResponseBase> CreateProduct(Product product, List<MaterialDto>? materials, List<Guid>? colorIds)
        {

            if (product.UrlImage != null)
            {
                if (!RegexUtils.IsValidImageUrl(product.UrlImage!))
                {
                    OperationNotCompletedResponseDto notCompletedResponse = new()
                    {
                        message = "Url da imagem não é compativel com o formato, verifique se a url contém: https, .png ou .jpg.",
                        statusCode = 400
                    };

                    return notCompletedResponse;
                }
            }

            if (!materials.Any())
            {
                OperationNotCompletedResponseDto notCompletedResponse = new()
                {
                    message = "É necessário associar um ou mais materiais para criar um produto.",
                    statusCode = 400
                };

                return notCompletedResponse;
            }

            var materialList = await _materialRepository.GetAllMaterialsOnTheList(materials);

            foreach (var materialItem in materialList)
            {
                _productRepository.AttachMaterial(materialItem);
                product.Materials.Add(materialItem);
            }

            if (colorIds != null && colorIds.Any())
            {

                var colorList = await _colorRepository.GetAllColorsOnTheList(colorIds);

                foreach (var colorItem in colorList)
                {
                    _productRepository.AttachColor(colorItem);
                    product.Colors.Add(colorItem);
                }
            }

            await _productRepository.CreateProduct(product);

            OperationCompletedResponseDto operationCompleted = new()
            {
                message = "Produto criado com sucesso!",
            };

            return operationCompleted;
        }

        public async Task<ResultResponseBase> DeleteProduct(ActionUserDto actionUser)
        {
            var product = await _productRepository.GetProductById(actionUser.productId);

            if (product == null)
            {
                OperationNotCompletedResponseDto notCompletedResponse = new()
                {
                    message = "Não foi possível encontrar o produto.",
                    statusCode = 404
                };

                return notCompletedResponse;
            }

            await _productRepository.DeleteProduct(product);

            OperationCompletedResponseDto operationCompleted = new()
            {
                message = "Produto deletado com sucesso!.",
            };

            return operationCompleted;
        }
    }
}