using EGeek.Identity.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EGeek.API.Configuration
{
    public class GlobalException
        : IExceptionHandler
    {
        private readonly ILogger<GlobalException> _logger;
        public GlobalException(
            ILogger<GlobalException> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Error: {Message}", exception.Message);

            var problemDetails = exception switch
            {
                EGeekIdentityException ex => new ProblemDetails
                {
                    Title = "Erros de validações.",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message
                },

                _ => new ProblemDetails
                {
                    Title = "Server internal Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Ocorreu um erro inesperado."
                }
            };

            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
