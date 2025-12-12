using Dapper;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Mappers;
using MySaaS.Infrastructure.Models;
using MySaaS.Infrastructure.Models.Querys;
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
            int recipeId = await _context.Connection.ExecuteScalarAsync<int>(RecipeSQL.Insert, new
            {
                RecipeId = obj.Id,
                Quantity_Unit_Id = obj.Quantity.UnitId,
                Quantity_Unit = obj.Quantity.Amount
            }, _context.Transaction);

            var ingredients = obj.Ingredients.Select(ri => new
            {
                Recipe_Id = recipeId,
                Ingredient_Id = ri.Ingredient.ItemId,
                Weight_Unit_Id = ri.Weight.UnitId,
                Weight_Quantity = ri.Weight.Amount,
                Waste_Unit_Id = ri.Waste.UnitId,
                Waste_Quantity = ri.Waste.Amount
            });

            await _context.Connection.ExecuteAsync(RecipeIngredientSQL.Insert, ingredients, _context.Transaction);

            return recipeId;
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<bool>(RecipeSQL.Exists, new { Id = objId }, _context.Transaction);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var result = await _context.Connection.QueryAsync<RecipeModel>(RecipeSQL.Select,
                _context.Transaction);
            return result.Map();
        }

        public async Task<IEnumerable<Recipe>> GetAllWithIngredientsAsync()
        {
            IEnumerable<Recipe> recipes;
            using (var multiple = await _context.Connection.QueryMultipleAsync(RecipeSQL.SelectWithIngredients, transaction: _context.Transaction))
            {
                recipes = (await multiple.ReadAsync<RecipeModel>()).Map().ToList();
                var components = (await multiple.ReadAsync<RecipeIngredientModel>());

                var lookup = components
                    .GroupBy(c => c.RecipeItemId)
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
            Recipe? recipe;
            using (var multiple = await _context.Connection.QueryMultipleAsync(RecipeSQL.SelectWithIngredientsById, new { RecipeId = objId }, transaction: _context.Transaction))
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
            return await _context.Connection.ExecuteAsync(RecipeSQL.Delete,
                new
                {
                    RecipeId = objId
                }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Recipe obj)
        {
            return await _context.Connection.ExecuteAsync(RecipeSQL.Update, new
            {
                RecipeId = obj.Id,
                Quantity_Unit_Id = obj.Quantity.UnitId,
                Quantity_Unit = obj.Quantity.Amount
            }, _context.Transaction);
        }
    }
}
