using Dapper;
using MySaaS.Application.Interfaces.Unities;
using MySaaS.Domain.Entities.Common;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Models.Querys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Repositories
{
    internal class UnitConversionRepository : IUnitConversionRepository
    {
        private readonly IDapperContext _context;

        public UnitConversionRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(UnitConversion obj)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(UnitConversionSQL.Insert,
                new
                {
                    ItemId = obj.ItemId,
                    FromUnitId = obj.FromUnitId,
                    ToUnitId = obj.ToUnitId,
                    ConversionFactor = obj.ConversionFactor
                }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<bool>(UnitConversionSQL.Exists,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<IEnumerable<UnitConversion>> GetAllAsync()
        {
            return await _context.Connection.QueryAsync<UnitConversion>(UnitConversionSQL.Select);
        }

        public async Task<UnitConversion?> GetByIdAsync(int objId)
        {
            return await _context.Connection.QuerySingleOrDefaultAsync<UnitConversion>(UnitConversionSQL.SelectById,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(UnitConversionSQL.Delete,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<int> UpdateAsync(UnitConversion obj)
        {
            return await _context.Connection.ExecuteAsync(UnitConversionSQL.Update,
                new
                {
                    Id = obj.Id,
                    ItemId = obj.ItemId,
                    FromUnitId = obj.FromUnitId,
                    ToUnitId = obj.ToUnitId,
                    ConversionFactor = obj.ConversionFactor
                }, _context.Transaction);
        }
    }
}
