using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;
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
                Amount = recipeModel.RecipeAmount,
                Unit = new Unit
                {
                    Id = recipeModel.UnitId,
                    Name = recipeModel.UnitName
                }
            };

            Item item = new Item()
            {
                Id = recipeModel.ItemId,
                Name = recipeModel.ItemName,
                Description = recipeModel.ItemDescription,
            };
            Recipe recipe = new Recipe
            {
                Id = recipeModel.ItemId,
                Item = item,
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
