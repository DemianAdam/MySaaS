using MySaaS.Application.Interfaces.Common.Tenancy;
using System;

namespace MySaaS.WebApi.Middlewares
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ITenantResolver tenantResolver, ITenantContext tenantContext)
        {
            HttpRequest request = httpContext.Request;
            string? path = request.Path.Value?.ToLower();
            if (path is not null && (path.StartsWith("/openapi") || path.StartsWith("/scalar")))
            {
                await _next(httpContext);
                return;
            }

            string host = request.Host.Host;
            string? tenantId = host.Split('.').FirstOrDefault();

            if (host == "localhost")
            {
                tenantId = request.Headers["X-Tenant-ID"].FirstOrDefault();
            }

            
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid tenant");
                return;
            }
            var tenant = await tenantResolver.ResolveAsync(tenantId);

            if (tenant is null)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Tenant not registered");
                return;
            }

            tenantContext.SetTenant(tenant.TenantId, tenant.ConnectionString);

            await _next(httpContext);
        }
    }
}
