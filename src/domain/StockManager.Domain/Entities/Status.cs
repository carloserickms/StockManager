namespace domain.StockManager.Domain.Entities
{
    public class Status : BaseModelId
    {
        public string Name { get; private set; }


        public ICollection<ServiceOrder> ServiceOrders { get; private set; } = new HashSet<ServiceOrder>();

        public Status(string name) : base()
        {
            Name = name;
        }
    }
}