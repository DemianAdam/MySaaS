using Dapper;
using MySaaS.Application.Interfaces.Items.Ingredients;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Infrastructure.Database;

namespace MySaaS.Infrastructure.Repositories
{
    internal class IngredientRepository : IIngredientRepository
    {
        private readonly IDapperContext _context;

        public IngredientRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Ingredient obj)
        {
            string sql =
                """
                    INSERT INTO Ingredients 
                    (id_item, id_recipe)
                    VALUES
                    (@ItemId, @RecipeId);
                """;

            await _context.Connection.ExecuteAsync(sql, new { ItemId = obj.ItemId, RecipeId = obj.Recipe?.Id }, _context.Transaction);

            return obj.ItemId;
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM supplies
                    WHERE supply_id = @SupplyId
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }


        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            string selectIngredients =
                $"""
                    SELECT 
                        it.item_id AS {nameof(Ingredient.Item.Id)},
                        it.name AS {nameof(Ingredient.Item.Name)},
                        it.description AS {nameof(Ingredient.Item.Description)},
                        r.recipe_id AS {nameof(Ingredient.Recipe.Id)},
                        r.name AS {nameof(Ingredient.Recipe.Name)},
                        r.quantity_unit_id AS {nameof(Ingredient.Recipe.Quantity.UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        u.unit_id AS {nameof(Ingredient.Recipe.Quantity.Unit.Id)},
                        u.name AS {nameof(Ingredient.Recipe.Quantity.Unit.Name)}
                    FROM ingredients AS i
                    LEFT JOIN items AS it ON it.supply_id = i.id_item
                    LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id
                """;
            return await _context.Connection.QueryAsync<Item, Recipe, Quantity, Unit, Ingredient>(selectIngredients, (it, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }

                return new Ingredient
                {
                    ItemId = it.Id,
                    Item = it,
                    Recipe = r
                };
            }, _context.Transaction,
            splitOn: $"{nameof(Ingredient.Recipe.Id)},{nameof(Ingredient.Recipe.Quantity.UnitId)},{nameof(Ingredient.Recipe.Quantity.Unit.Id)}");
        }

        public async Task<Ingredient?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT 
                        SELECT 
                        it.item_id AS {nameof(Ingredient.Item.Id)},
                        it.name AS {nameof(Ingredient.Item.Name)},
                        it.description AS {nameof(Ingredient.Item.Description)},
                        r.recipe_id AS {nameof(Ingredient.Recipe.Id)},
                        r.name AS {nameof(Ingredient.Recipe.Name)},
                        r.quantity_unit_id AS {nameof(Ingredient.Recipe.Quantity.UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        u.unit_id AS {nameof(Ingredient.Recipe.Quantity.Unit.Id)},
                        u.name AS {nameof(Ingredient.Recipe.Quantity.Unit.Name)}
                    FROM ingredients AS i
                    JOIN items AS it ON it.item_id = i.id_item
                    LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id
                    WHERE i.id_item = @Id
                """;
            //TODO: maybe add recursive ingredient fetching?
            var result = await _context.Connection.QueryAsync<Item, Recipe, Quantity, Unit, Ingredient>(sql, (it, r, q, u) =>
             {
                 if (r is not null)
                 {
                     r.Quantity = q;
                     r.Quantity.Unit = u;
                 }

                 return new Ingredient
                 {
                     ItemId = it.Id,
                     Item = it,
                     Recipe = r
                 };
             }, _context.Transaction,
             splitOn: $"{nameof(Ingredient.Recipe.Id)},{nameof(Ingredient.Recipe.Quantity.UnitId)},{nameof(Ingredient.Recipe.Quantity.Unit.Id)}");

            return result.FirstOrDefault();
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql =
                """
                    DELETE FROM ingredients 
                    WHERE id_supply = @Id
                """;

            return await _context.Connection.ExecuteAsync(sql,
               new
               {
                   Id = objId
               }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Ingredient obj)
        {
            string sql =
                """
                    UPDATE ingredients
                    SET id_recipe = @RecipeId
                    WHERE id_item = @ItemId
                """;
            return await _context.Connection.ExecuteAsync(sql, new { ItemId = obj.ItemId, RecipeId = obj.Recipe?.Id }, _context.Transaction);
        }
    }
}
