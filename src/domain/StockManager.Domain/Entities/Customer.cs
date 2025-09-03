using shared.StockManager.Shered;

namespace domain.StockManager.Domain.Entities
{
    public class Customer : BaseModelId
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }


        public Customer() : base() { }

        public Customer(string name, string phone) : base()
        {
            Name = name;
            Phone = phone;
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