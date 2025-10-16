


using application.StockManager.Application.Dtos;

namespace application.StockManager.Application.responses
{
    public class MaterialResponseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public decimal value { get; set; }
        public List<ColorDto> colors { get; set; } = new List<ColorDto>();
    }
}