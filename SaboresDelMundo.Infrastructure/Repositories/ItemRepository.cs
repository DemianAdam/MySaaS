using Dapper;
using MySaaS.Application.Interfaces.Common.Items;
using MySaaS.Domain.Entities.Common;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Models.Querys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Repositories
{
    internal class ItemRepository : IItemRepository
    {
        private readonly IDapperContext _context;

        public ItemRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Item obj)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(ItemSQL.Insert,
                new
                {
                    Name = obj.Name,
                    Description = obj.Description
                }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.ExecuteScalarAsync<bool>(ItemSQL.Exists, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Connection.QueryAsync<Item>(ItemSQL.Select);
        }

        public async Task<Item?> GetByIdAsync(int objId)
        {
            return await _context.Connection.QuerySingleOrDefaultAsync<Item>(ItemSQL.SelectById, new { Id = objId }, _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(ItemSQL.Delete, new { Id = objId }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Item obj)
        {
            return await _context.Connection.ExecuteAsync(ItemSQL.Update,
                new
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Description = obj.Description
                }, _context.Transaction);
        }
    }
}
