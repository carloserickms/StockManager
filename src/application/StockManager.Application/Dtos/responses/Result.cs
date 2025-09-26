namespace application.StockManager.Application.responses
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Message { get; }

        private Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Message = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Created(string message) => new Result<T>(true, default, message);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
    }
}

