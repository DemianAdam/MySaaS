using Dapper;
using MySaaS.Application.Interfaces.Products;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly IDapperContext _context;
        public ProductRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Product obj)
        {
            string sql =
                """
                    INSERT INTO products
                    (id_item, id_recipe)
                    VALUES
                    (@ItemId, @RecipeId)
                """;

            return await _context.Connection.ExecuteScalarAsync<int>(sql, new
            {
                ItemId = obj.ItemId,
                RecipeId = obj.Recipe?.Id
            }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM products
                    WHERE id_item = @Id
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            string sql =
                $"""
                SELECT
                    it.item_id AS {nameof(Product.Item.Id)},
                    it.name AS {nameof(Product.Item.Name)},
                    it.description AS {nameof(Product.Item.Description)},
                    p.price AS {nameof(Product.Price)},
                    r.recipe_id AS {nameof(Product.Recipe.Id)},
                    r.name AS {nameof(Product.Recipe.Name)},
                    r.quantity_unit_id AS {nameof(Product.Recipe.Quantity.UnitId)},
                    r.quantity AS {nameof(Product.Recipe.Quantity.Amount)},
                    u.unit_id AS {nameof(Product.Recipe.Quantity.Unit.Id)},
                    u.name AS {nameof(Product.Recipe.Quantity.Unit.Name)}
                FROM products AS p
                LEFT JOIN items AS it ON p.id_item = it.item_id
                LEFT JOIN recipes AS r ON p.id_recipe = r.recipe_id
                LEFT JOIN unities AS u ON r.quantity_unit_id = u.unit_id
                """;
            return await _context.Connection.QueryAsync<Item, decimal, Recipe, Quantity, Unit, Product>(sql, (item, price, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }
                return new Product
                {
                    ItemId = item.Id,
                    Item = item,
                    Price = price,
                    Recipe = r
                };
            }, transaction: _context.Transaction,
            splitOn: $" {nameof(Product.Price)},{nameof(Product.Recipe.Id)},{nameof(Product.Recipe.Quantity.UnitId)},{nameof(Product.Recipe.Quantity.Unit.Id)}");
        }

        public async Task<Product?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                SELECT
                    it.item_id AS {nameof(Product.Item.Id)},
                    it.name AS {nameof(Product.Item.Name)},
                    it.description AS {nameof(Product.Item.Description)},
                    p.price AS {nameof(Product.Price)},
                    r.recipe_id AS {nameof(Product.Recipe.Id)},
                    r.name AS {nameof(Product.Recipe.Name)},
                    r.quantity_unit_id AS {nameof(Product.Recipe.Quantity.UnitId)},
                    r.quantity AS {nameof(Product.Recipe.Quantity.Amount)},
                    u.unit_id AS {nameof(Product.Recipe.Quantity.Unit.Id)},
                    u.name AS {nameof(Product.Recipe.Quantity.Unit.Name)}
                FROM products AS p
                LEFT JOIN items AS it ON p.id_item = it.item_id
                LEFT JOIN recipes AS r ON p.id_recipe = r.recipe_id
                LEFT JOIN unities AS u ON r.quantity_unit_id = u.unit_id
                WHERE p.id_item = @Id
                """;

            var result = await _context.Connection.QueryAsync<Item, decimal, Recipe, Quantity, Unit, Product>(sql, (item, price, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }
                return new Product
                {
                    ItemId = item.Id,
                    Item = item,
                    Price = price,
                    Recipe = r
                };
            }, transaction: _context.Transaction,
            splitOn: $" {nameof(Product.Price)},{nameof(Product.Recipe.Id)},{nameof(Product.Recipe.Quantity.UnitId)},{nameof(Product.Recipe.Quantity.Unit.Id)}");

            return result.FirstOrDefault();
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql =
                """
                    DELETE FROM products
                    WHERE id_item = @Id;
                """;
            return await _context.Connection.ExecuteAsync(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Product obj)
        {
            string sql =
                """
                    UPDATE products
                    SET
                        price = @Price,
                        id_recipe = @RecipeId
                    WHERE id_item = @Id;
                """;
            return await _context.Connection.ExecuteAsync(sql, new
            {
                Id = obj.ItemId,
                Price = obj.Price,
                RecipeId = obj.Recipe?.Id
            }, _context.Transaction);
        }
    }
}
