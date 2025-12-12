using Dapper;
using Microsoft.Data.SqlClient;
using MySaaS.Tests.Factory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApiFactory>, IAsyncLifetime
    {
        private readonly WebApiFactory _factory;

        public WebApiFactory Factory { get => _factory; }

        public BaseIntegrationTest(WebApiFactory factory)
        {
            _factory = factory;
        }
        public Task DisposeAsync() => Task.CompletedTask;

        public async Task InitializeAsync()
        {
            _factory.CreateClient();

            using var connection = new NpgsqlConnection(_factory.ConnectionString);
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
