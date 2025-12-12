using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class RecipeComponentMapper
    {
        public static RecipeComponent Map(this RecipeIngredientModel recipeIngredientRow)
        {
            Ingredient ingredient = new Ingredient
            {
                ItemId = recipeIngredientRow.IngredientItemId,
                Item = new Item
                {
                    Id = recipeIngredientRow.IngredientItemId,
                    Name = recipeIngredientRow.IngredientItemName,
                    Description = recipeIngredientRow.IngredientItemDescription
                },
            };

            if (recipeIngredientRow.HasRecipe())
            {
                Unit recipeUnit = new Unit
                {
                    Id = recipeIngredientRow.Ingredient_RecipeUnitId!.Value,
                    Name = recipeIngredientRow.Ingredient_RecipeUnitName!
                };

                Quantity recipeQuantity = new Quantity
                {
                    UnitId = recipeUnit.Id,
                    Amount = recipeIngredientRow.Ingredient_RecipeAmount!.Value,
                    Unit = recipeUnit
                };

                Item item = new Item()
                {
                    Id = recipeIngredientRow.Ingredient_RecipeItemId!.Value,
                    Name = recipeIngredientRow.Ingredient_RecipeItemName!,
                    Description = recipeIngredientRow.Ingredient_RecipeItemDescription!
                };

                ingredient.Recipe = new Recipe
                {
                    Id = item.Id,
                    Item = item,
                    Quantity = recipeQuantity,
                };
            }

            Quantity weight = new Quantity
            {
                UnitId = recipeIngredientRow.Weight_UnitId,
                Amount = recipeIngredientRow.Weight_Amount,
                Unit = new Unit
                {
                    Id = recipeIngredientRow.Weight_UnitId,
                    Name = recipeIngredientRow.Weight_Unit_Name
                }
            };

            Quantity waste = new Quantity
            {
                UnitId = recipeIngredientRow.Waste_UnitId,
                Amount = recipeIngredientRow.Waste_Amount,
                Unit = new Unit
                {
                    Id = recipeIngredientRow.Waste_UnitId,
                    Name = recipeIngredientRow.Waste_Unit_Name
                }
            };

            RecipeComponent recipeComponent = new RecipeComponent
            {
                Ingredient = ingredient,
                Weight = weight,
                Waste = waste
            };

            return recipeComponent;
        }

        public static IEnumerable<RecipeComponent> Map(this IEnumerable<RecipeIngredientModel> recipeIngredientRows)
        {
            foreach (var row in recipeIngredientRows)
            {
                yield return row.Map();
            }
        }
    }
}
