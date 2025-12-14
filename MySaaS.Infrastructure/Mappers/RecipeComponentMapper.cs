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
                Id = recipeIngredientRow.Id,
                Ingredient = ingredient,
                Weight = weight,
                Waste = waste
            };

            return recipeComponent;
        }
        public static RecipeIngredientModel Map(this RecipeComponent recipeComponent,Recipe recipe)
        {
            return new RecipeIngredientModel()
            {
                Id = recipeComponent.Id,
                RecipeItemId = recipe.Id,
                RecipeItemName = recipe.Item!.Name,
                RecipeItemDescription = recipe.Item.Description,
                IngredientItemId = recipeComponent.Ingredient.ItemId,
                IngredientItemName = recipeComponent.Ingredient.Item!.Name,
                IngredientItemDescription = recipeComponent.Ingredient.Item.Description,
                Ingredient_RecipeItemId = recipeComponent.Ingredient.Recipe!.Id,
                Ingredient_RecipeItemName = recipeComponent.Ingredient.Recipe.Item!.Name,
                Ingredient_RecipeItemDescription = recipeComponent.Ingredient.Recipe.Item.Description,
                Ingredient_RecipeAmount = recipeComponent.Ingredient.Recipe.Quantity.Amount,
                Ingredient_RecipeUnitId = recipeComponent.Ingredient.Recipe.Quantity.UnitId,
                Ingredient_RecipeUnitName = recipeComponent.Ingredient.Recipe.Quantity.Unit!.Name,
                Weight_UnitId = recipeComponent.Weight.UnitId,
                Weight_Amount = recipeComponent.Weight.Amount,
                Weight_Unit_Name = recipeComponent.Weight.Unit!.Name,
                Waste_UnitId = recipeComponent.Waste.UnitId,
                Waste_Amount = recipeComponent.Waste.Amount,
                Waste_Unit_Name = recipeComponent.Waste.Unit!.Name
            };
        }
        public static IEnumerable<RecipeIngredientModel> Map(this IEnumerable<RecipeComponent> recipeComponents,Recipe recipe)
        {
            return recipeComponents.Select(x => x.Map(recipe));
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
