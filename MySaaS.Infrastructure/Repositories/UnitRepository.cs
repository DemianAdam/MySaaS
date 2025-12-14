using Dapper;
using MySaaS.Application.Interfaces.Common.Unities;
using MySaaS.Domain.Entities.Common;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Models.Querys;

namespace MySaaS.Infrastructure.Repositories
{
    internal class UnitRepository : IUnitRepository
    {
        private readonly IDapperContext _context;

        public UnitRepository(IDapperContext dapperUnitOfWork)
        {
            _context = dapperUnitOfWork;
        }

        public async Task<int> AddAsync(Unit unity)
        {
            return await _context.Connection.ExecuteScalarAsync<int>(UnitSQL.Insert,
                 new { Name = unity.Name },
                 _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<bool>(UnitSQL.Exists,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _context.Connection.QueryAsync<Unit>(UnitSQL.Select);
        }

        public async Task<Unit?> GetByIdAsync(int objId)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<Unit>(UnitSQL.SelectById,
                new { Id = objId }, _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(UnitSQL.Delete,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<int> UpdateAsync(Unit unity)
        {
            return await _context.Connection.ExecuteAsync(UnitSQL.Update,
                 new { Id = unity.Id, Name = unity.Name },
                 _context.Transaction);
        }
    }
}
