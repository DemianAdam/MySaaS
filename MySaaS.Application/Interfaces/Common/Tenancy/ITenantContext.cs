using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Common.Tenancy
{
    public interface ITenantContext
    {
        string? TenantId { get; }
        string? ConnectionString { get; }
        void SetTenant(string tenantId, string connectionString);
    }
}
