using Dapper;
using MySaaS.Application.Interfaces.Products.Products;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Domain.Entities.Products;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Mappers;
using MySaaS.Infrastructure.Models;
using MySaaS.Infrastructure.Models.Querys;
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
            return await _context.Connection.ExecuteScalarAsync<int>(ProductSQL.Insert, new
            {
                ItemId = obj.ItemId,
                Price = obj.Price,
                RecipeId = obj.RecipeId
            }, _context.Transaction);
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<bool>(ProductSQL.Exists, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> products;
            using (var multiple = await _context.Connection.QueryMultipleAsync(ProductSQL.SelectWithCategories))
            {
                products = (await multiple.ReadAsync<ProductModel>()).Map();
                var categories = (await multiple.ReadAsync<ProductCategoryModel>());

                var lookup = categories
                    .GroupBy(c => c.ProductId)
                    .ToDictionary(g => g.Key,g => g.ToList());

                foreach (var product in products)
                {
                    if(lookup.TryGetValue(product.ItemId, out var list))
                    {
                        IEnumerable<Category> productCategories = list.Map();
                        product.UpdateCategories(productCategories);
                    }
                }
                    
            }
            return products;
        }

        public async Task<Product?> GetByIdAsync(int objId)
        {
            var result = await _context.Connection.QueryAsync<Item, decimal, Recipe, Quantity, Unit, Product>(ProductSQL.SelectById, (item, price, r, q, u) =>
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
            return await _context.Connection.ExecuteAsync(ProductSQL.Delete, new { Id = objId }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Product obj)
        {
            return await _context.Connection.ExecuteAsync(ProductSQL.Update, new
            {
                Id = obj.ItemId,
                Price = obj.Price,
                RecipeId = obj.RecipeId
            }, _context.Transaction);
        }
    }
}
