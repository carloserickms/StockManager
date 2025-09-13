namespace application.StockManager.Application.responses
{
    public class MaterialResponseDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public decimal value { get; set; }
    }
}