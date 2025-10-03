using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/ServiceOrder")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto orderDto)
        {
            var resultOrder = await _orderService.CreateOrder(orderDto, orderDto.products);

            if (!resultOrder.IsSuccess)
            {
                ApiResponse.Fail(resultOrder.Message, 400);
            }

            return ApiResponse.Created(resultOrder.Message, 201);
        }
    }
}