using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class RecipeMapper
    {
        public static Recipe Map(this RecipeModel recipeModel)
        {
            Quantity quantity = new Quantity
            {
                UnitId = recipeModel.UnitId,
                Amount = recipeModel.Amount,
                Unit = new Unit
                {
                    Id = recipeModel.UnitId,
                    Name = recipeModel.UnitName
                }
            };

            Recipe recipe = new Recipe
            {
                Id = recipeModel.Id,
                Name = recipeModel.Name,
                Quantity = quantity,
            };

            return recipe;
        }

        public static IEnumerable<Recipe> Map(this IEnumerable<RecipeModel> recipeModels)
        {
            return recipeModels.Select(rm => rm.Map());
        }
    }
}
