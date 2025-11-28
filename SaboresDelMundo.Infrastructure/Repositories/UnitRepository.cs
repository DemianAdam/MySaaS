using Dapper;
using MySaaS.Application.Interfaces.Unities;
using MySaaS.Domain.Entities;
using MySaaS.Infrastructure.Database;

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
            string sql =
                """
                INSERT INTO unities 
                (name)
                VALUES (@Name)
                RETURNING unit_id;
                """;
            return await _context.Connection.ExecuteScalarAsync<int>(sql,
                 new { Name = unity.Name },
                 _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM unities
                    WHERE unit_id = @Id
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            string sql =
                $"""
                    SELECT 
                    unit_id AS {nameof(Unit.Id)},
                    name AS {nameof(Unit.Name)} 
                    FROM unities
                """;
            return await _context.Connection.QueryAsync<Unit>(sql);
        }

        public async Task<Unit?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT 
                        unit_id AS {nameof(Unit.Id)},
                        name AS {nameof(Unit.Name)} 
                    FROM unities
                    WHERE unit_id = @Id;
                """;

            return await _context.Connection.QueryFirstOrDefaultAsync<Unit>(sql,
                new { Id = objId }, _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql = "DELETE FROM unities WHERE unit_id = @Id";
            return await _context.Connection.ExecuteAsync(sql,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<int> UpdateAsync(Unit unity)
        {
            string sql = "UPDATE unities SET name = @Name WHERE unit_Id = @Id";
            return await _context.Connection.ExecuteAsync(sql,
                 new { Id = unity.Id, Name = unity.Name },
                 _context.Transaction);
        }
    }
}
