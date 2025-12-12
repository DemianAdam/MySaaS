using MySaaS.Application.Interfaces.Common.Tenancy;

namespace MySaaS.Tests.Mock
{
    internal class MockTenantContext : ITenantContext
    {
        public string? TenantId { get; private set; }

        public string? ConnectionString { get; private set; }

        public void SetTenant(string tenantId, string connectionString)
        {
            this.TenantId = tenantId;
            this.ConnectionString = connectionString;
        }
    }
}
