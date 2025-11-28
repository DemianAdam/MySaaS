using Dapper;
using MySaaS.Application.Interfaces.Supplies;
using MySaaS.Domain.Entities;
using MySaaS.Infrastructure.Database;

namespace MySaaS.Infrastructure.Repositories
{
    internal class SupplyRepository : ISupplyRepository
    {
        private readonly IDapperContext _context;

        public SupplyRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Supply obj)
        {
            string sql =
                """
                    INSERT INTO supplies 
                    (name,description)
                    VALUES
                    (@Name,@Description)
                    RETURNING supply_id;
                """;

            return await _context.Connection.ExecuteScalarAsync<int>(sql, new
            {
                Name = obj.Name,
                Description = obj.Description,
            }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM supplies
                    WHERE supply_id = @Id
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Supply>> GetAllAsync()
        {
            string sql =
                $"""
                    SELECT 
                    supply_id AS {nameof(Supply.Id)},
                    name AS {nameof(Supply.Name)},
                    description AS {nameof(Supply.Description)}
                    FROM supplies;
                """;
            return await _context.Connection.QueryAsync<Supply>(sql);
        }

        public Task<Supply?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT 
                        supply_id AS {nameof(Supply.Id)},
                        name AS {nameof(Supply.Name)},
                        description AS {nameof(Supply.Description)}
                    FROM supplies
                    WHERE supply_id = @Id;
                """;

            return _context.Connection.QuerySingleOrDefaultAsync<Supply>(sql,
                new { Id = objId }, _context.Transaction);
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql = "DELETE FROM supplies WHERE supply_id = @Id";
            return await _context.Connection.ExecuteAsync(sql,
                new { Id = objId },
                _context.Transaction);
        }

        public async Task<int> UpdateAsync(Supply obj)
        {
            string sql = "UPDATE supplies SET name = @Name,description = @Description WHERE supply_id = @Id";
            return await _context.Connection.ExecuteAsync(sql,
                new
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Description = obj.Description
                },
                _context.Transaction);
        }
    }
}
