using Microsoft.AspNetCore.Mvc;

namespace shared.StockManager.Shered.Helper
{
    public static class ApiResponse
    {
        public static IActionResult Success<T>(T data) =>
            new ObjectResult(new { statusCode = 200, data }) { StatusCode = 200 };

        public static IActionResult Created<T>(T data) =>
            new ObjectResult(new { statusCode = 201, data }) { StatusCode = 201 };

        public static IActionResult Fail(string message, int statusCode = 400) =>
            new ObjectResult(new { statusCode, message }) { StatusCode = statusCode };

        public static IActionResult NotFound(string message) =>
            new ObjectResult(new { statusCode = 404, message }) { StatusCode = 404 };
    }
}
