using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/material")]
    public class MaterialController : ControllerBase
    {
        private IMaterialService _materialSerivice;

        public MaterialController(IMaterialService materialSerivice)
        {
            _materialSerivice = materialSerivice;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMaterialDto createMaterial)
        {
            var resultMaterial = await _materialSerivice.CreateMaterial(createMaterial.ToMaterial(), createMaterial.ColorIds);

            if (resultMaterial.IsSuccess == false)
            {
                ApiResponse.Fail(resultMaterial.Message, 400);
            }

            return ApiResponse.Created(resultMaterial.Message, 201);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page)
        {
            var resultMaterial = await _materialSerivice.GetAllMaterial(page);

            if (resultMaterial.IsSuccess == false)
            {
                ApiResponse.NotFound(resultMaterial.Message, 404);
            }

            return ApiResponse.Success(resultMaterial.Value, "Todos os materiais foram encontrados.", 200);
        }

        [HttpGet("getByName")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var resultMaterial = await _materialSerivice.GetMaterialByName(search);

            if (resultMaterial.IsSuccess == false)
            {
                ApiResponse.NotFound(resultMaterial.Message, 404);
            }

            return ApiResponse.Success(resultMaterial.Value, "Todos os materiais foram encontrados.", 200);
        }
    }
}