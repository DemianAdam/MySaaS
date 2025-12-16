using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySaaS.Application.Interfaces.Common.Tenancy;
using MySaaS.Tests.Mock;
using MySaaS.WebApi;
using Npgsql;


namespace MySaaS.Tests.Factory
{
    public class WebApiFactory : WebApplicationFactory<IPublic>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddUserSecrets<MockTenantResolver>();
            });

            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<ITenantResolver>();
                services.RemoveAll<ITenantContext>();
                services.AddSingleton<ITenantResolver, MockTenantResolver>();
                services.AddSingleton<ITenantContext, MockTenantContext>();
            });
        }

        protected override void ConfigureClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-Tenant-ID", "testtenant");
        }

        private string GetConnectionString()
        {
            using var scope = Services.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            string? connectionString = configuration["Tests:ConnectionString"];

            if (connectionString is null)
            {
                throw new InvalidOperationException("Tests:ConnectionString not configured.");
            }

            return connectionString;
        }

        public async Task ResetDatabaseAsync()
        {
            string connectionString = GetConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            string sql =
                """
                    DO
                    $$
                    DECLARE
                        stmt TEXT;
                    BEGIN
                        SELECT 'TRUNCATE TABLE ' ||
                               string_agg(format('%I.%I', schemaname, tablename), ', ') ||
                               ' RESTART IDENTITY CASCADE;'
                        INTO stmt
                        FROM pg_tables
                        WHERE schemaname NOT IN ('pg_catalog', 'information_schema');

                        EXECUTE stmt;
                    END
                    $$;
                """;
            await connection.ExecuteAsync(sql);
        }
    }
}
