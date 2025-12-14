using MySaaS.Application.Interfaces.Common.Tenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Database.Tenancy
{
    internal class TenantContext : ITenantContext
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
