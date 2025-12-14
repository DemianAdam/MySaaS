using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class IngredientMapper
    {
        public static Ingredient Map(this IngredientModel model)
        {
            Item ingredientItem = new Item()
            {
                Id = model.ItemId,
                Name = model.ItemName,
                Description = model.ItemDescription,
            };

            Recipe? recipe = GetRecipe(model);

            return new Ingredient()
            {
                ItemId = ingredientItem.Id,
                Item = ingredientItem,
                Recipe = recipe
            };
        }

        public static IEnumerable<Ingredient> Map(this IEnumerable<IngredientModel> ingredientModels)
        {
            return ingredientModels.Select(Map);
        }

        private static Recipe? GetRecipe(IngredientModel model)
        {
            if (!model.HasRecipe())
            {
                return null;
            }

            return new RecipeModel()
            {
                ItemId = model.RecipeItemId!.Value,
                RecipeAmount = model.RecipeAmount!.Value,
                ItemName = model.RecipeItemName!,
                ItemDescription = model.RecipeItemDescription,
                UnitId = model.RecipeUnitId!.Value,
                UnitName = model.RecipeUnitName!,
            }.Map();
        }
    }
}
