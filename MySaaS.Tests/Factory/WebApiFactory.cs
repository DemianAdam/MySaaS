using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySaaS.Application.Interfaces.Common.Tenancy;
using MySaaS.Tests.Mock;
using MySaaS.WebApi;


namespace MySaaS.Tests.Factory
{
    public class WebApiFactory : WebApplicationFactory<IPublic>
    {
        public string ConnectionString { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<ITenantResolver>();
                services.RemoveAll<ITenantContext>();

                MockTenantResolver resolver = new MockTenantResolver();
                var result = resolver.ResolveAsync("testtenant").Result;
                ConnectionString = result!.ConnectionString;

                services.AddSingleton<ITenantResolver>(resolver);
                services.AddSingleton<ITenantContext, MockTenantContext>();
            });
        }

        protected override void ConfigureClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-Tenant-ID", "testtenant");
        }
    }
}
