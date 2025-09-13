using application.StockManager.Application.Dtos;

namespace application.StockManager.Application.responses
{
    public class OperationCompletedResponseDto : ResultResponseBase
    {
        public override string message { get; set; }
    }
}