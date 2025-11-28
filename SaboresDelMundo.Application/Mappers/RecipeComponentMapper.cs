using MySaaS.Application.DTOs.Recipes.Components;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Mappers
{
    internal static class RecipeComponentMapper
    {
        public static RecipeComponentDTO Map(this RecipeComponent recipeComponent)
        {
            return new RecipeComponentDTO
            {
                Ingredient = recipeComponent.Ingredient.Map(),
                Weight = recipeComponent.Weight.Map(),
                Waste = recipeComponent.Waste.Map()
            };
        }

        public static RecipeComponent Map(this RecipeComponentDTO recipeComponentDTO)
        {
            return new RecipeComponent
            {
                Ingredient = recipeComponentDTO.Ingredient.Map(),
                Weight = recipeComponentDTO.Weight.Map(),
                Waste = recipeComponentDTO.Waste.Map()
            };
        }
        
        public static RecipeComponent Map(this CreateRecipeComponentDTO createRecipeComponentDTO)
        {
            return new RecipeComponent
            {
                Ingredient = new Ingredient { SupplyId = createRecipeComponentDTO.IngredientId },
                Weight = createRecipeComponentDTO.Weight.Map(),
                Waste = createRecipeComponentDTO.Waste.Map()
            };
        }
        public static IEnumerable<RecipeComponentDTO> Map(this IEnumerable<RecipeComponent> recipeComponents)
        {
            return recipeComponents.Select(recipeComponent => recipeComponent.Map());
        }

        public static IEnumerable<RecipeComponent> Map(this IEnumerable<RecipeComponentDTO> recipeComponentDTOs)
        {
            return recipeComponentDTOs.Select(recipeComponentDTO => recipeComponentDTO.Map());
        }

        public static IEnumerable<RecipeComponent> Map(this IEnumerable<CreateRecipeComponentDTO> createRecipeComponentDTOs)
        {
            return createRecipeComponentDTOs.Select(createRecipeComponentDTO => createRecipeComponentDTO.Map());
        }
    }
}
