using MySaaS.Application.DTOs.Common.Tenancy;
using MySaaS.Application.Interfaces.Common.Tenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Tests.Mock
{
    internal class MockTenantResolver : ITenantResolver
    {
        public Task<TenantInfo?> ResolveAsync(string tenantId)
        {
            TenantInfo tenantInfo = new TenantInfo()
            {
                TenantId = tenantId,
                ConnectionString = "Host=localhost;Port=5432;Database=MySaaS_Tests;Username=postgres;Password=7441893a;"
            };

            return Task.FromResult(tenantInfo)!;
        }
    }
}
