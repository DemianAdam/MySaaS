using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production;
using MySaaS.Domain.Entities.Production.Recipes;
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
            if(recipe.Item is null)
            {
                throw new ArgumentNullException(nameof(recipe.Item), "Item property cannot be null when mapping Recipe to RecipeDTO.");
            }
            return new RecipeDTO
            {
                Id = recipe.Id,
                Name = recipe.Item.Name,
                Ingredients = recipe.Ingredients.Map().ToList(),
                Quantity = recipe.Quantity.Map(),
            };
        }
        public static Recipe Map(this RecipeDTO recipeDTO)
        {
            Item item = new Item()
            { 
                Id = recipeDTO.Id,
                Name = recipeDTO.Name,
                Description = recipeDTO.Description
            };
            Recipe recipe = new Recipe(recipeDTO.Ingredients.Map())
            {
                Id = recipeDTO.Id,
                Item = item,
                Quantity = recipeDTO.Quantity.Map(),
            };

            return recipe;
        }

        public static Recipe Map(this CreateRecipeDTO createRecipeDTO)
        {
            Item item = new Item()
            {
                Name = createRecipeDTO.Name,
                Description = createRecipeDTO.Description
            };
            Recipe recipe = new Recipe(createRecipeDTO.RecipeInfo.Ingredients.Map())
            {
                Id = item.Id,
                Item = item,
                Quantity = createRecipeDTO.RecipeInfo.Quantity.Map(),
            };
            return recipe;
        }

        public static Recipe Map(this UpdateRecipeDTO updateRecipeDTO)
        {
            Item item = new Item()
            {
                Id = updateRecipeDTO.Id,
                Name = updateRecipeDTO.Name,
                Description = updateRecipeDTO.Description
            };
            Recipe recipe = new Recipe(updateRecipeDTO.RecipeInfo.Ingredients.Map())
            {
                Id = updateRecipeDTO.Id,
                Item = item,
                Quantity = updateRecipeDTO.RecipeInfo.Quantity.Map(),
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
