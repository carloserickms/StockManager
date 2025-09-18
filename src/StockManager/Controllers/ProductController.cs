using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
using application.StockManager.Application.responses;
using application.StockManager.Application.Service;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto createProduct)
        {

            var product = await _productService.CreateProduct(createProduct.ToProduct(), createProduct.Materials, createProduct.ColorIds);

            if (product is OperationNotCompletedResponseDto productCast)
            {
                return productCast.statusCode == 400 ? ApiResponse.Fail(productCast.message, productCast.statusCode) : ApiResponse.NotFound(productCast.message, productCast.statusCode);
            }

            return ApiResponse.Created(product.message, 201);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int productId)
        {
            ActionUserDto actionUser = new()
            {
                productId = productId
            };

            var result = await _productService.DeleteProduct(actionUser);

            if (result is OperationNotCompletedResponseDto resultCast)
            {
                return resultCast.statusCode == 400 ? ApiResponse.Fail(resultCast.message, resultCast.statusCode) : ApiResponse.NotFound(resultCast.message, resultCast.statusCode);
            }

            return ApiResponse.Created(result.message, 201);
        }
    }
}