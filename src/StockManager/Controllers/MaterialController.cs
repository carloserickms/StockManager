using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces;
using application.StockManager.Application.responses;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/material")]
    public class MaterialController : ControllerBase
    {
        private IMaterialSerivice _materialSerivice;

        public MaterialController(IMaterialSerivice materialSerivice)
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
        public async Task<IActionResult> Get()
        {
            var resultMaterial = await _materialSerivice.GetAllMaterial();

            return ApiResponse.Success(resultMaterial, 200);
        }
    }
}