using application.StockManager.Application.Interfaces;
using application.StockManager.Application.responses;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered;

namespace application.StockManager.Application.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IColorRepository _colorRepository;

        public ProductService(IProductRepository productRepository, IColorRepository colorRepository)
        {
            _productRepository = productRepository;
            _colorRepository = colorRepository;
        }


        public async Task<ProductResponseDto> CreateProduct(Product product, List<Guid>? materialIds, List<Guid>? colorIds)
        {

            if (product.UrlImage != null)
            {
                if (!RegexUtils.IsValidImageUrl(product.UrlImage!))
                {
                    throw new ArgumentException("Url da imagem não é compativel com o formato, verifique se a url contém: https, .png ou .jpg.");
                }
            }

            if (colorIds != null && colorIds.Any())
            {
                foreach (var colorId in colorIds)
                {
                    var color = await _colorRepository.GetColorById(colorId);
                    product.Colors.Add(color);
                }
            }

            await _productRepository.CreateProduct(product);

            ProductResponseDto productResponse = new()
            {
                name = product.Name,
                amount = product.Amount,
                discount = product.Discount,
                urlImage = product.UrlImage,
                value = product.Value,
                colors = product.Colors
                        .Select(c => new ColorResponseDto
                        {
                            Id = c.Id,
                            Name = c.Name
                        })
                        .ToList()
            };

            return productResponse;
        }
    }
}