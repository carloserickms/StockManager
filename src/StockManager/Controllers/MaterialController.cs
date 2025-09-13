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
            var material = await _materialSerivice.CreateMaterial(createMaterial.ToMaterial(), createMaterial.ColorIds);

            if (material is OperationNotCompletedResponseDto materialCast)
            {
                return materialCast.statusCode == 400 ? ApiResponse.Fail(materialCast.message, materialCast.statusCode) : ApiResponse.NotFound(materialCast.message, materialCast.statusCode);
            }

            return ApiResponse.Created(material.message, 201);
        }
    }
}