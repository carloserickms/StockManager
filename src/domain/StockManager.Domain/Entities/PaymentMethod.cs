namespace domain.StockManager.Domain.Entities
{
    public class PaymentMethod : BaseModelId
    {
        public string Name { get; private set; }


        public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new HashSet<ServiceOrder>();

        public PaymentMethod(string name) : base()
        {
            Name = name;
        }
    }
}