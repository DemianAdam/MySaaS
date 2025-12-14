using Dapper;
using MySaaS.Application.Interfaces.Purchases.PurchaseItems;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Inventory;
using MySaaS.Domain.Entities.Purchases;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Mappers;
using MySaaS.Infrastructure.Models;
using MySaaS.Infrastructure.Models.Querys;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MySaaS.Infrastructure.Repositories
{
    internal class PurchaseItemRepository : IPurchaseItemRepository
    {
        private readonly IDapperContext _context;

        public PurchaseItemRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(PurchaseItem obj)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(PurchaseItemSQL.Insert,
                new
                {
                    PurchaseId = obj.PurchaseId,
                    ItemId = obj.ItemId,
                    UnitId = obj.UnitId,
                    Quantity = obj.Quantity,
                    Cost = obj.Cost,
                    MovementId = obj.MovementId,
                }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.ExecuteScalarAsync<bool>(PurchaseItemSQL.Exists,
                new
                {
                    Id = objId,
                }, _context.Transaction);
        }

        public async Task<IEnumerable<PurchaseItem>> GetAllAsync()
        {
            var result = await _context.Connection.QueryAsync<PurchaseItemModel>(PurchaseItemSQL.Select,
                _context.Transaction);

            return result.Map();
        }

        public async Task<PurchaseItem?> GetByIdAsync(int objId)
        {
            var result = await _context.Connection.QueryAsync<PurchaseItemModel>(PurchaseItemSQL.SelectById,
                 _context.Transaction);

            return result.FirstOrDefault()?.Map();
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(PurchaseItemSQL.Delete);
        }

        public async Task<int> UpdateAsync(PurchaseItem obj)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(PurchaseItemSQL.Update,
                new
                {
                    Id = obj.Id,
                    PurchaseId = obj.PurchaseId,
                    ItemId = obj.ItemId,
                    UnitId = obj.UnitId,
                    Quantity = obj.Quantity,
                    Cost = obj.Cost,
                    MovementId = obj.MovementId,
                }, _context.Transaction);
        }
    }
}
