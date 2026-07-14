using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InfosoftGlobalTestApi.Domain.Exceptions; 

namespace InfosoftGlobalTestApi.Api.Middlewares
{
    public class GlobalExceptionMiddleware : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Error occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            switch (exception)
            {
                case BreederNotFoundException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status404NotFound, "Resource not found", ex.Message);
                    break;

                case LitterNotFoundException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status404NotFound, "Resource not found", ex.Message);
                    break;

                case BenefitLimitException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status403Forbidden, "Benefit limit exceeded", ex.Message);
                    break;

                case BenefitDataException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status400BadRequest, "Business rule violation", ex.Message);
                    break;

                case InvalidLitterStatusException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status400BadRequest, "Business rule violation", ex.Message);
                    break;

                case LitterNoOwnException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status400BadRequest, "Business rule violation", ex.Message);
                    break;

                case KeyNotFoundException ex:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status404NotFound, "Resource not found", ex.Message);
                    break;

                default:
                    SetResponse(httpContext, problemDetails, StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred on our side.");
                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private void SetResponse(HttpContext context, ProblemDetails problemDetails, int statusCode, string title, string detail)
        {
            context.Response.StatusCode = statusCode;
            problemDetails.Status = statusCode;
            problemDetails.Title = title;
            problemDetails.Detail = detail;
        }
    }
}