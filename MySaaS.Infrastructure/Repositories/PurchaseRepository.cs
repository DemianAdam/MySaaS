using Dapper;
using MySaaS.Application.Interfaces.Purchases;
using MySaaS.Domain.Entities.Purchases;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Models.Querys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Repositories
{
    internal class PurchaseRepository : IPurchaseRepository
    {
        private readonly IDapperContext _context;

        public PurchaseRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Purchase obj)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(PurchaseSQL.Insert,
                new
                {
                    Date = obj.Date,
                }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.ExecuteScalarAsync<bool>(PurchaseSQL.Exists,
                new
                {
                    Id = objId
                }, _context.Transaction);
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _context.Connection.QueryAsync<Purchase>(PurchaseSQL.Select, transaction: _context.Transaction);
        }

        public async Task<Purchase?> GetByIdAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<Purchase>(PurchaseSQL.SelectById,
                new
                {
                    Id = objId
                }, _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(PurchaseSQL.Delete,
                new
                {
                    Id = objId
                },
                _context.Transaction);
        }

        public async Task<int> UpdateAsync(Purchase obj)
        {
            return await _context.Connection.ExecuteAsync(PurchaseSQL.Update,
                new
                {
                    Id = obj.Id,
                }, _context.Transaction);
        }
    }
}
