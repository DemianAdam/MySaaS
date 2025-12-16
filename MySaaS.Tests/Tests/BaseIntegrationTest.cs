using Dapper;
using Microsoft.Data.SqlClient;
using MySaaS.Tests.Factory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Tests.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApiFactory>, IAsyncLifetime
    {
        protected WebApiFactory Factory { get; }

        public BaseIntegrationTest(WebApiFactory factory)
        {
            Factory = factory;
        }
        public Task DisposeAsync() => Task.CompletedTask;

        public async Task InitializeAsync()
        {
            await Factory.ResetDatabaseAsync();
        }
    }
}
