using Dapper;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Mappers;
using MySaaS.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace MySaaS.Infrastructure.Repositories
{
    internal class RecipeRepository : IRecipeRepository
    {
        private readonly IDapperContext _context;
        public RecipeRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Recipe obj)
        {
            string sql =
                """
                    INSERT INTO recipes
                    (name, quantity_unit_id, quantity)
                    VALUES 
                    (@Name, @Quantity_Unit_Id, @Quantity_Unit)
                    RETURNING recipe_id;
                """;

            string ingredientsSql =
                """
                    INSERT INTO recipe_ingredients
                    (id_recipe, id_ingredient, weight_unit_id, weight_quantity, waste_unit_id, waste_quantity)
                    VALUES 
                    (@Recipe_Id, @Ingredient_Id, @Weight_Unit_Id, @Weight_Quantity, @Waste_Unit_Id, @Waste_Quantity);
                """;

            int recipeId = await _context.Connection.ExecuteScalarAsync<int>(sql, new
            {
                Name = obj.Name,
                Quantity_Unit_Id = obj.Quantity.UnitId,
                Quantity_Unit = obj.Quantity.Amount
            }, _context.Transaction);

            var ingredients = obj.Ingredients.Select(ri => new
            {
                Recipe_Id = recipeId,
                Ingredient_Id = ri.Ingredient.SupplyId,
                Weight_Unit_Id = ri.Weight.UnitId,
                Weight_Quantity = ri.Weight.Amount,
                Waste_Unit_Id = ri.Waste.UnitId,
                Waste_Quantity = ri.Waste.Amount
            });

            await _context.Connection.ExecuteAsync(ingredientsSql, ingredients, _context.Transaction);

            return recipeId;
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM recipes
                    WHERE recipe_id = @Id
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            string sql =
                $"""
                    SELECT
                        r.recipe_id AS {nameof(Recipe.Id)},
                        r.name AS {nameof(Recipe.Name)},
                        r.quantity_unit_id AS {nameof(Recipe.Quantity.UnitId)},
                        r.quantity AS {nameof(Recipe.Quantity.Amount)},
                        u.unit_id as {nameof(Recipe.Quantity.Unit.Id)},
                        u.name as {nameof(Recipe.Quantity.Unit.Name)}
                    FROM recipes AS r
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id
                """;



            return await _context.Connection.QueryAsync<Recipe, Quantity, Unit, Recipe>(sql,
                (r, q, u) =>
                {
                    q.Unit = u;
                    r.Quantity = q;
                    return r;
                },
                _context.Transaction,
                splitOn: $"{nameof(Recipe.Quantity.UnitId)},{nameof(Recipe.Quantity.Unit.Id)}"
            );
        }

        public async Task<IEnumerable<Recipe>> GetAllWithIngredientsAsync()
        {
            string sql =
                $"""
                    SELECT
                        r.recipe_id AS {nameof(RecipeModel.Id)},
                        r.name AS {nameof(RecipeModel.Name)},
                        r.quantity_unit_id AS {nameof(RecipeModel.UnitId)},
                        r.quantity AS {nameof(RecipeModel.Amount)},
                        u.name as {nameof(RecipeModel.UnitName)}
                    FROM recipes AS r
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id;

                    SELECT
                        ri.id_recipe AS {nameof(RecipeIngredientModel.RecipeId)},
                        ri.id_ingredient AS {nameof(RecipeIngredientModel.Supply_Id)},
                        s.name AS {nameof(RecipeIngredientModel.Supply_Name)},
                        s.description AS {nameof(RecipeIngredientModel.Supply_Description)},
                        r.recipe_id AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Id)},
                        r.name AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Name)},
                        r.quantity_unit_id AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Quantity_UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        ur.name AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Quantity_Unit_Name)},
                        ri.weight_unit_id AS {nameof(RecipeIngredientModel.Weight_UnitId)},
                        ri.weight_quantity AS {nameof(RecipeIngredientModel.Weight_Amount)},
                        uwe.name AS {nameof(RecipeIngredientModel.Weight_Unit_Name)},
                        ri.waste_unit_id AS {nameof(RecipeIngredientModel.Waste_UnitId)},
                        ri.waste_quantity AS {nameof(RecipeIngredientModel.Waste_Amount)},
                        uwa.name AS {nameof(RecipeIngredientModel.Waste_Unit_Name)}
                    FROM recipe_ingredients AS ri
                        LEFT JOIN supplies AS s ON s.supply_id = ri.id_ingredient
                        LEFT JOIN ingredients AS i ON i.id_supply = s.supply_id
                        LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                        LEFT JOIN unities AS ur ON ur.unit_id = r.quantity_unit_id
                        LEFT JOIN unities AS uwe ON uwe.unit_id = ri.weight_unit_id
                        LEFT JOIN unities AS uwa ON uwa.unit_id = ri.waste_unit_id;
                """;

            IEnumerable<Recipe> recipes;
            using (var multiple = await _context.Connection.QueryMultipleAsync(sql, transaction: _context.Transaction))
            {
                recipes = (await multiple.ReadAsync<RecipeModel>()).Map().ToList();
                var components = (await multiple.ReadAsync<RecipeIngredientModel>()).ToList();

                var lookup = components
                    .GroupBy(c => c.RecipeId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var recipe in recipes)
                {
                    if (lookup.TryGetValue(recipe.Id, out var list))
                    {
                        IEnumerable<RecipeComponent> recipeComponents = list.Map();
                        recipe.UpdateComponents(recipeComponents);
                    }
                }
            }

            return recipes;
        }

        public async Task<Recipe?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT
                        r.recipe_id AS {nameof(RecipeModel.Id)},
                        r.name AS {nameof(RecipeModel.Name)},
                        r.quantity_unit_id AS {nameof(RecipeModel.UnitId)},
                        r.quantity AS {nameof(RecipeModel.Amount)},
                        u.name as {nameof(RecipeModel.UnitName)}
                    FROM recipes AS r
                        LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id;
                    WHERE r.recipe_id = @RecipeId;

                    SELECT
                        ri.id_recipe AS {nameof(RecipeIngredientModel.RecipeId)},
                        ri.id_ingredient AS {nameof(RecipeIngredientModel.Supply_Id)},
                        s.name AS {nameof(RecipeIngredientModel.Supply_Name)},
                        s.description AS {nameof(RecipeIngredientModel.Supply_Description)},
                        r.recipe_id AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Id)},
                        r.name AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Name)},
                        r.quantity_unit_id AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Quantity_UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        ur.name AS {nameof(RecipeIngredientModel.Ingredient_Recipe_Quantity_Unit_Name)},
                        ri.weight_unit_id AS {nameof(RecipeIngredientModel.Weight_UnitId)},
                        ri.weight_quantity AS {nameof(RecipeIngredientModel.Weight_Amount)},
                        uwe.name AS {nameof(RecipeIngredientModel.Weight_Unit_Name)},
                        ri.waste_unit_id AS {nameof(RecipeIngredientModel.Waste_UnitId)},
                        ri.waste_quantity AS {nameof(RecipeIngredientModel.Waste_Amount)},
                        uwa.name AS {nameof(RecipeIngredientModel.Waste_Unit_Name)}
                    FROM recipe_ingredients AS ri
                        LEFT JOIN supplies AS s ON s.supply_id = ri.id_ingredient
                        LEFT JOIN ingredients AS i ON i.id_supply = s.supply_id
                        LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                        LEFT JOIN unities AS ur ON ur.unit_id = r.quantity_unit_id
                        LEFT JOIN unities AS uwe ON uwe.unit_id = ri.weight_unit_id
                        LEFT JOIN unities AS uwa ON uwa.unit_id = ri.waste_unit_id;
                    WHERE ri.id_recipe = @RecipeId;
                """;

            Recipe? recipe;
            using (var multiple = await _context.Connection.QueryMultipleAsync(sql, transaction: _context.Transaction))
            {
                recipe = (await multiple.ReadAsync<RecipeModel>()).FirstOrDefault()?.Map();
                if (recipe is not null)
                {
                    var components = (await multiple.ReadAsync<RecipeIngredientModel>()).ToList().Map();
                    recipe.UpdateComponents(components);
                }
            }

            return recipe;
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql = "DELETE FROM recipes WHERE recipe_id = @RecipeId;";
            return await _context.Connection.ExecuteAsync(sql,
                new
                {
                    RecipeId = objId
                }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Recipe obj)
        {
            string sql =
                """
                    UPDATE recipes
                    SET name = @Name,
                        quantity_unit_id = @Quantity_Unit_Id,
                        quantity = @Quantity_Unit
                    WHERE recipe_id = @RecipeId;
                """;
            return await _context.Connection.ExecuteAsync(sql, new
            {
                RecipeId = obj.Id,
                Name = obj.Name,
                Quantity_Unit_Id = obj.Quantity.UnitId,
                Quantity_Unit = obj.Quantity.Amount
            }, _context.Transaction);
        }
    }
}
