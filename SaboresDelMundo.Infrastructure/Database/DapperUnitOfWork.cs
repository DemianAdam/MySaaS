using Microsoft.IdentityModel.Tokens;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Application.Interfaces.Common.Tenancy;
using Npgsql;
using System.Data;

namespace MySaaS.Infrastructure.Database
{
    internal class DapperUnitOfWork : IUnitOfWork, IDapperContext
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;
        private bool _disposed;

        public IDbConnection Connection => _connection;
        public IDbTransaction? Transaction => _transaction;

        public DapperUnitOfWork(ITenantContext tenantContext)
        {
            if(string.IsNullOrEmpty(tenantContext.ConnectionString))
            {
                throw new ArgumentNullException(nameof(tenantContext.ConnectionString), "Tenant connection string cannot be null or empty.");
            }

            _connection = new NpgsqlConnection(tenantContext.ConnectionString);
            _connection.Open();
        }
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
                _disposed = true;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
