using application.StockManager.Application.responses;

namespace application.StockManager.Application.Dtos
{
    public abstract class ResultResponseBase
    {
        public abstract string message { get; set; }
    }
}