using MySaaS.Application.DTOs.Recipes;
using MySaaS.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Mappers
{
    public static class RecipeMapper
    {
        public static RecipeDTO Map(this Recipe recipe)
        {
            return new RecipeDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Ingredients = recipe.Ingredients.Map().ToList(),
                Quantity = recipe.Quantity.Map(),
            };
        }
        public static Recipe Map(this RecipeDTO recipeDTO)
        {
            Recipe recipe = new Recipe(recipeDTO.Ingredients.Map())
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name,
                Quantity = recipeDTO.Quantity.Map(),
            };

            return recipe;
        }

        public static Recipe Map(this CreateRecipeDTO createRecipeDTO)
        {
            Recipe recipe = new Recipe(createRecipeDTO.Ingredients.Map())
            {
                Name = createRecipeDTO.Name,
                Quantity = createRecipeDTO.Quantity.Map(),
            };
            return recipe;
        }

        public static Recipe Map(this UpdateRecipeDTO updateRecipeDTO)
        {
            Recipe recipe = new Recipe(updateRecipeDTO.Ingredients.Map())
            {
                Id = updateRecipeDTO.Id,
                Name = updateRecipeDTO.Name,
                Quantity = updateRecipeDTO.Quantity.Map(),
            };

            return recipe;
        }

        public static IEnumerable<RecipeDTO> Map(this IEnumerable<Recipe> recipes)
        {
            return recipes.Select(recipe => recipe.Map());
        }
        public static IEnumerable<Recipe> Map(this IEnumerable<RecipeDTO> recipeDTOs)
        {
            return recipeDTOs.Select(recipeDTO => recipeDTO.Map());
        }
    }
}
