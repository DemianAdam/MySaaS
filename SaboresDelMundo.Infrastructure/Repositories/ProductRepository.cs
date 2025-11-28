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
                    (name, description, price, id_recipe)
                    VALUES
                    (@Name, @Description, @Price, @RecipeId)
                    RETURNING product_id;
                """;

            return await _context.Connection.ExecuteScalarAsync<int>(sql, new
            {
                Name = obj.Name,
                Description = obj.Description,
                Price = obj.Price,
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
                    WHERE product_id = @Id
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            string sql =
                $"""
                SELECT
                    p.product_id AS {nameof(Product.Id)},
                    p.name AS {nameof(Product.Name)},
                    p.description AS {nameof(Product.Description)},
                    p.price AS {nameof(Product.Price)},
                    r.recipe_id AS {nameof(Product.Recipe.Id)},
                    r.name AS {nameof(Product.Recipe.Name)},
                    r.quantity_unit_id AS {nameof(Product.Recipe.Quantity.UnitId)},
                    r.quantity AS {nameof(Product.Recipe.Quantity.Amount)},
                    u.unit_id AS {nameof(Product.Recipe.Quantity.Unit.Id)},
                    u.name AS {nameof(Product.Recipe.Quantity.Unit.Name)}
                FROM products AS p
                LEFT JOIN recipes AS r ON p.id_recipe = r.recipe_id
                LEFT JOIN unities AS u ON r.quantity_unit_id = u.unit_id
                """;
            return await _context.Connection.QueryAsync<Product, Recipe, Quantity, Unit, Product>(sql, (p, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }
                p.Recipe = r;
                return p;
            }, transaction: _context.Transaction,
            splitOn: $"{nameof(Product.Recipe.Id)},{nameof(Product.Recipe.Quantity.UnitId)},{nameof(Product.Recipe.Quantity.Unit.Id)}");
        }

        public async Task<Product?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                SELECT
                    p.product_id AS {nameof(Product.Id)},
                    p.name AS {nameof(Product.Name)},
                    p.description AS {nameof(Product.Description)},
                    p.price AS {nameof(Product.Price)},
                    r.recipe_id AS {nameof(Product.Recipe.Id)},
                    r.name AS {nameof(Product.Recipe.Name)},
                    r.quantity_unit_id AS {nameof(Product.Recipe.Quantity.UnitId)},
                    r.quantity AS {nameof(Product.Recipe.Quantity.Amount)},
                    u.unit_id AS {nameof(Product.Recipe.Quantity.Unit.Id)},
                    u.name AS {nameof(Product.Recipe.Quantity.Unit.Name)}
                FROM products AS p
                LEFT JOIN recipes AS r ON p.id_recipe = r.recipe_id
                LEFT JOIN unities AS u ON r.quantity_unit_id = u.unit_id
                WHERE p.product_id = @Id
                """;

            var result = await _context.Connection.QueryAsync<Product, Recipe, Quantity, Unit, Product>(sql, (p, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }
                p.Recipe = r;
                return p;
            }, transaction: _context.Transaction,
            splitOn: $"{nameof(Product.Recipe.Id)},{nameof(Product.Recipe.Quantity.UnitId)},{nameof(Product.Recipe.Quantity.Unit.Id)}");
            
            return result.FirstOrDefault();
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql =
                """
                    DELETE FROM products
                    WHERE product_id = @Id;
                """;
            return await _context.Connection.ExecuteAsync(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Product obj)
        {
            string sql =
                """
                    UPDATE products
                    SET
                        name = @Name,
                        description = @Description,
                        price = @Price,
                        id_recipe = @RecipeId
                    WHERE product_id = @Id;
                """;
            return await _context.Connection.ExecuteAsync(sql, new
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Price = obj.Price,
                RecipeId = obj.Recipe?.Id
            }, _context.Transaction);
        }
    }
}
