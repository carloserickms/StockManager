using System.Net;
using System.Text.Json;
using Domain.StockManager.Domain.Exceptions;

namespace StockManager.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message = exception.Message;

            if (exception is BusinessException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            var response = new
            {
                statusCode = (int)statusCode,
                message
            };

            var json = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(json);
        }
    }
}