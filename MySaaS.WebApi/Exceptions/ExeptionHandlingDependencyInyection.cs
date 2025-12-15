namespace MySaaS.WebApi.Exceptions
{
    public static class ExeptionHandlingDependencyInyection
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails(config =>
            {
                config.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });
            return services;
        }
    }
}
