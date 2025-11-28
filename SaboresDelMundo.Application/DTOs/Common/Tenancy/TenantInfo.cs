using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.Tenancy
{
    public class TenantInfo
    {
        public required string TenantId { get; init; }
        public required string ConnectionString { get; init; }
    }
}
