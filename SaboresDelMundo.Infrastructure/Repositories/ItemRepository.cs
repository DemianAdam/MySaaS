using Dapper;
using MySaaS.Application.Interfaces.Items;
using MySaaS.Domain.Entities;
using MySaaS.Infrastructure.Database;
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
            string sql =
                """
                    INSERT INTO items
                        (name,description)
                    VALUES
                        (@Name,@Description)
                    RETURNING item_id;
                """;

            return await _context.Connection.ExecuteScalarAsync<int>(sql,
                new
                {
                    Name = obj.Name,
                    Description = obj.Description
                }, _context.Transaction);
        }

        public Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM items
                    WHERE item_id = @Id
                );
                """;

            return _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            string sql =
                $"""
                    SELECT 
                    item_id AS {nameof(Item.Id)},
                    name AS {nameof(Item.Name)},
                    description AS {nameof(Item.Description)}
                    FROM items;
                """;
            return _context.Connection.QueryAsync<Item>(sql);
        }

        public Task<Item?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT 
                    item_id AS {nameof(Item.Id)},
                    name AS {nameof(Item.Name)},
                    description AS {nameof(Item.Description)}
                    FROM items
                    WHERE item_id = @Id;
                """;
            return _context.Connection.QuerySingleOrDefaultAsync<Item>(sql, new { Id = objId }, _context.Transaction);
        }

        public Task<int> RemoveAsync(int objId)
        {
            string sql =
                """
                    DELETE FROM items
                    WHERE item_id = @Id;
                """;
            return _context.Connection.ExecuteAsync(sql, new { Id = objId }, _context.Transaction);
        }

        public Task<int> UpdateAsync(Item obj)
        {
            string sql =
                """
                    UPDATE items
                    SET
                        name = @Name,
                        description = @Description
                    WHERE item_id = @Id;
                """;
            return _context.Connection.ExecuteAsync(sql,
                new
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Description = obj.Description
                }, _context.Transaction);
        }
    }
}
