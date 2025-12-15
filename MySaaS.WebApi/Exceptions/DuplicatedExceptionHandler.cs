using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Domain.Exceptions.Common;

namespace MySaaS.WebApi.Exceptions
{
    public class DuplicatedExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        public DuplicatedExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            _problemDetailsService = problemDetailsService;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not DuplicatedException notFoundException)
            {
                return false;
            }

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            ProblemDetailsContext problemDetailsContext = new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails()
                {
                    Type = exception.GetType().Name,
                    Detail = exception.Message,
                }
            };
            return await _problemDetailsService.TryWriteAsync(problemDetailsContext);
        }
    }
}
