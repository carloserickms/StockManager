using application.StockManager.Application.Dtos.responses;
using application.StockManager.Application.Dtos.resquests;
using domain.StockManager.Domain.Entities;
using shared.StockManager.Shered.Utils;

namespace application.StockManager.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        public Task<Result<Customer>> CreateCustomer(CreateCustomerDto customer);
        public Task<Result<IEnumerable<CustomerResponseDto>>> GetAllCustomer();
    }
}