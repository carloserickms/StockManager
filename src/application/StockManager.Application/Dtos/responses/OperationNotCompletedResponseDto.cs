using application.StockManager.Application.Dtos;

namespace application.StockManager.Application.responses
{
    public class OperationNotCompletedResponseDto : ResultResponseBase
    {
        public override int statusCode { get; set; }
        public override string message { get; set; }
    }
}