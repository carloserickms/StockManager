using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Service;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto createProduct)
        {
            var product = await _productService.CreateProduct(createProduct.ToProduct(), createProduct.MaterialIds, createProduct.ColorIds);
            return ApiResponse.Created(product);
        }
    }
}