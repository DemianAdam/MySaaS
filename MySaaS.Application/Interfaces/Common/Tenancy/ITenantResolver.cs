using MySaaS.Application.DTOs.Common.Tenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Common.Tenancy
{
    public interface ITenantResolver
    {
        Task<TenantInfo?> ResolveAsync(string tenantId);
    }
}
