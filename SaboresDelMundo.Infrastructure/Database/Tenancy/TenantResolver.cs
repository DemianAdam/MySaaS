using Dapper;
using MySaaS.Application.DTOs.Common.Tenancy;
using MySaaS.Application.Interfaces.Common.Tenancy;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySaaS.Infrastructure.Database.Tenancy
{
    internal class TenantResolver : ITenantResolver
    {
        private readonly string _connectionString;
        public TenantResolver(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<TenantInfo?> ResolveAsync(string tenantId)
        {
            const string query =
                $"""
                    SELECT 
                        tenant_id AS {nameof(TenantInfo.TenantId)},
                        connection_string AS {nameof(TenantInfo.ConnectionString)}
                    FROM
                        tenants
                    WHERE
                        tenant_id = @TenantId
                """;

            TenantInfo? result = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryFirstOrDefaultAsync<TenantInfo>(query, new { TenantId = tenantId });
            }

            return result;
        }
    }
}
