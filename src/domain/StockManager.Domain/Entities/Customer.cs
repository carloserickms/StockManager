using domain.StockManager.Domain.Entities.ValueObjects;
using Domain.StockManager.Domain.Exceptions;
using shared.StockManager.Shered;

namespace domain.StockManager.Domain.Entities
{
    public class Customer : BaseModelId
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Region { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }


        public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new HashSet<ServiceOrder>();

        public Customer() : base() { }

        public Customer(string name, string phone, string region, DateTime dateOfBirth) : base()
        {
            Name = name;
            Phone = phone;
            Region = region;
            DateOfBirth = dateOfBirth;
        }


        public static Customer Create(CustomerInfo customerInfo)
        {
            if (String.IsNullOrEmpty(customerInfo.Name))
            {
                throw new BusinessException("O campo nome não pode esta vazio");
            }

            if (!RegexUtils.IsValidBrazilPhone(customerInfo.Phone))
            {
                throw new BusinessException("Erro ao tentar validar número, verifique.");
            }

            if (customerInfo.DateOfBirth == default)
            {
                throw new BusinessException("O campo data de nascimento não pode estar vazio.");
            }

            Customer customer = new(customerInfo.Name, customerInfo.Phone, customerInfo.Region, customerInfo.DateOfBirth);

            customer.Gender = customerInfo.Gender;

            return customer;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("campo 'nome' não pode ser vazio");
            }

            Name = name;
        }

        public void UpdatePhone(string phone)
        {
            if (!RegexUtils.IsValidBrazilPhone(phone))
            {
                throw new ArgumentException("campo 'telefone' não pode ser vazio");
            }

            Name = phone;
        }
    }
}