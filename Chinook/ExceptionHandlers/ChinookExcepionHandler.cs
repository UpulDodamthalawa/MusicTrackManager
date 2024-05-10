using Chinook.ExceptionHandlers;
using Microsoft.AspNetCore.Diagnostics;

namespace Chinook.ExceptionHandler
{
    public class ChinookExcepionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                ForbidException => (403, null),
                BadRequestException badRequestException => (400, badRequestException.Message),
                NotFoundException notFoundException => (404, notFoundException.Message),
                _ => (500, "An unexpected error occured")
            };

            return true;
        }
    }
}
