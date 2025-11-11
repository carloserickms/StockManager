using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Dtos.resquests;
using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using domain.StockManager.Domain.Entities;
using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Service
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IGenderRepository _genderRepository;

        public CustomerService(ICustomerRepository customerRepository, IGenderRepository genderRepository)
        {
            _customerRepository = customerRepository;
            _genderRepository = genderRepository;
        }

        public async Task<Result<Customer>> CreateCustomer(CreateCustomerDto customer)
        {
            try
            {
                var gender = await _genderRepository.GetById(customer.gender);

                CustomerInfo customerInfo = new(customer.name, customer.phone, customer.region, customer.dateOfBirth, gender);

                var newCustomer = Customer.Create(customerInfo);

                await _customerRepository.Save(newCustomer);
            }
            catch (BusinessException ex)
            {
                return Result<Customer>.Failure(ex.Message);
            }

            return Result<Customer>.Created("Cliente criado com sucesso.");
        }


        public async Task<Result<IEnumerable<CustomerResponseDto>>> GetAllCustomer()
        {
            try
            {
                var customersList = await _customerRepository.GetAll();

                if (customersList == null || !customersList.Any())
                {
                    return Result<IEnumerable<CustomerResponseDto>>.Failure("Nenhum cliente encontrado");
                }

                List<CustomerResponseDto> list = new();

                foreach (var item in customersList)
                {
                    var responseDto = new CustomerResponseDto
                    {
                        id = item.Id,
                        name = item.Name,
                        phone = item.Phone,
                        dateOfBirth = item.DateOfBirth,
                        region = item.Region,
                    };
                };

                return Result<IEnumerable<CustomerResponseDto>>.Success(list);
            }
            catch (BusinessException ex)
            {
                return Result<IEnumerable<CustomerResponseDto>>.Failure(ex.Message);
            }
        }
    }
}