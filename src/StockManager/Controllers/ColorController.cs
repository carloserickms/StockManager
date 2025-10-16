using application.StockManager.Application.Service;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/color")]
    public class ColorController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultColor = await _colorService.GetAllColors();

            if (resultColor.IsSuccess == false)
            {
                ApiResponse.Fail(resultColor.Message, 400);
            }

            return ApiResponse.Success(resultColor.Value, resultColor.Message, 200);
        }
    }
}