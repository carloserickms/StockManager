using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using shared.StockManager.Shered.Helper;

namespace StockManager.Controllers
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;
        private readonly IGenderService _genderService;

        public CustomerController(ICustomerService customerService, IGenderService genderService)
        {
            _customerService = customerService;
            _genderService = genderService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerDto createCustomer)
        {

            Console.WriteLine(createCustomer.dateOfBirth);

            var resultCustomer = await _customerService.CreateCustomer(createCustomer);

            if (resultCustomer.IsSuccess == false)
            {
                return ApiResponse.Fail(resultCustomer.Message, 400);
            }

            return ApiResponse.Created(resultCustomer.Message, 201);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultCustomer = await _customerService.GetAllCustomer();

            if (resultCustomer.IsSuccess == false)
            {
                return ApiResponse.Fail(resultCustomer.Message, 400);
            }

            return ApiResponse.Success(resultCustomer, resultCustomer.Message, 200);
        }


        [HttpGet("getAllGender")]
        public async Task<IActionResult> GetGender()
        {
            var resultGender = await _genderService.GetAllGender();

            if (resultGender.IsSuccess == false)
            {
                return ApiResponse.Fail(resultGender.Message, 400);
            }

            return ApiResponse.Success(resultGender, resultGender.Message, 200);
        }
    }
}