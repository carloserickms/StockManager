namespace application.StockManager.Application.Dtos.responses
{
    public class CustomerResponseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string region { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}