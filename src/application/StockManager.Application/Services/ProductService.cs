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


        public async Task<ResultResponseBase> CreateProduct(Product product, List<MaterialDto>? materials, List<int>? colorIds)
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

            if (materials == null || !materials.Any())
            {
                OperationNotCompletedResponseDto notCompletedResponse = new()
                {
                    message = "É necessário associar um ou mais materiais para criar um produto.",
                    statusCode = 400
                };

                return notCompletedResponse;
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

            var materialList = await _materialRepository.GetAllMaterialsOnTheList(materials);

            if (!materialList.Any() || !materials.Any())
            {
                OperationNotCompletedResponseDto operationNotCompleted = new()
                {
                    message = "Nenhum material pode ser associado.",
                    statusCode = 400
                };

                return operationNotCompleted;
            }

            double maxProdruct = ProductionCalculator.CheckAvailableQuantity(product, materialList, materials);

            product.UpdateAmount(maxProdruct);

            if (maxProdruct < 1)
            {
                OperationNotCompletedResponseDto operationNotCompleted = new()
                {
                    message = "Quantidade de material não é suficiente.",
                    statusCode = 400
                };

                return operationNotCompleted;
            }

            foreach (var materialListItem in materialList)
            {
                foreach (var materialsRequeridItem in materials)
                {
                    if (materialListItem.Id == materialsRequeridItem.id)
                    {
                        materialListItem.ReduceAmount(materialsRequeridItem.amount * maxProdruct);

                        product.Materials.Add(materialListItem);

                        _productRepository.AttachMaterial(materialListItem);
                        await _materialRepository.UpdateMaterial(materialListItem);
                    }
                }
            }

            await _productRepository.CreateProduct(product);

            OperationCompletedResponseDto operationCompleted = new()
            {
                message = $"Produto criado com sucesso!",
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