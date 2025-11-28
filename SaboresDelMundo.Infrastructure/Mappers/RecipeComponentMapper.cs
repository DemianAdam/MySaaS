using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
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
                SupplyId = recipeIngredientRow.Supply_Id,
                Supply = new Supply
                {
                    Id = recipeIngredientRow.Supply_Id,
                    Name = recipeIngredientRow.Supply_Name,
                    Description = recipeIngredientRow.Supply_Description
                },
            };

            if (recipeIngredientRow.HasRecipe())
            {
                Unit recipeUnit = new Unit
                {
                   Id = recipeIngredientRow.Ingredient_Recipe_Quantity_UnitId!.Value,
                   Name = recipeIngredientRow.Ingredient_Recipe_Quantity_Unit_Name
                };

                Quantity recipeQuantity = new Quantity
                {
                    UnitId = recipeUnit.Id,
                    Amount = recipeIngredientRow.Ingredient_Recipe_Quantity_Amount!.Value,
                    Unit = recipeUnit
                };


                ingredient.Recipe = new Recipe
                {
                    Id = recipeIngredientRow.Ingredient_Recipe_Id!.Value,
                    Name = recipeIngredientRow.Ingredient_Recipe_Name!,
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
