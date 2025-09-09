using Microsoft.AspNetCore.Mvc;

namespace shared.StockManager.Shered.Helper
{
    public static class ApiResponse
    {
        public static IActionResult Success<T>(T data, int statusCode) =>
            new ObjectResult(new { statusCode, data }) { StatusCode = 200 };

        public static IActionResult Created<T>(T data, int statusCode) =>
            new ObjectResult(new { statusCode, data }) { StatusCode = 201 };

        public static IActionResult Fail(string message, int statusCode ) =>
            new ObjectResult(new { statusCode, message }) { StatusCode = 400 };

        public static IActionResult NotFound(string message, int statusCode) =>
            new ObjectResult(new { statusCode, message }) { StatusCode = 404 };
    }
}
