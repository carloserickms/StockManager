using application.StockManager.Application.Dtos;
using application.StockManager.Application.Interfaces;

namespace application.StockManager.Application.responses
{
    public class OperationNotCompletedResponseDto : ResultResponseBase
    {
        public override string message { get; set; }
        public int statusCode { get; set; }
    }
}