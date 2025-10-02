using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
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

            var resultProduct = await _productService.CreateProduct(createProduct, createProduct.Materials, createProduct.ColorIds);

            if (resultProduct.IsSuccess == false)
            {
                return ApiResponse.Fail(resultProduct.Message, 400);
            }

            return ApiResponse.Created(resultProduct.Message, 201);
        }
    }
}