using Microsoft.Extensions.Configuration;
using MySaaS.Application.DTOs.Common.Tenancy;
using MySaaS.Application.Interfaces.Common.Tenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Tests.Mock
{
    internal class MockTenantResolver : ITenantResolver
    {
        private readonly IConfiguration _configuration;
        public MockTenantResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<TenantInfo?> ResolveAsync(string tenantId)
        {
            string? connectionString = _configuration["Tests:ConnectionString"];

            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string for tests is not configured.");
            }
            TenantInfo tenantInfo = new TenantInfo()
            {
                TenantId = tenantId,
                ConnectionString = connectionString
            };

            return Task.FromResult(tenantInfo)!;
        }
    }
}
