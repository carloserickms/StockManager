namespace application.StockManager.Application.Dtos.resquests
{
    public class CreateCustomerDto
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string region { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int gender { get; set; }
    }
}