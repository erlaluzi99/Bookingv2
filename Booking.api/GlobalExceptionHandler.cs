using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Booking.api
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask HandleAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, "An unhandled exception occurred.");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";

            var result = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Error = exception.Message // optionally hide this in production
            };

            await context.HttpContext.Response.WriteAsJsonAsync(result);
        }
    }
}
