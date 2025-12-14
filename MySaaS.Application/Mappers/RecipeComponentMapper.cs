using MySaaS.Application.DTOs.Production.Recipes.Components;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Production;
using MySaaS.Domain.Entities.Production.Recipes;
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
                Id = recipeComponent.Id,
                Ingredient = recipeComponent.Ingredient.Map(),
                Weight = recipeComponent.Weight.Map(),
                Waste = recipeComponent.Waste.Map()
            };
        }

        public static RecipeComponent Map(this RecipeComponentDTO recipeComponentDTO)
        {
            return new RecipeComponent
            {
                Id= recipeComponentDTO.Id,
                Ingredient = recipeComponentDTO.Ingredient.Map(),
                Weight = recipeComponentDTO.Weight.Map(),
                Waste = recipeComponentDTO.Waste.Map()
            };
        }
        
        public static RecipeComponent Map(this CreateRecipeComponentDTO createRecipeComponentDTO)
        {
            return new RecipeComponent
            {
                Ingredient = new Ingredient { ItemId = createRecipeComponentDTO.IngredientId },
                Weight = createRecipeComponentDTO.Weight.Map(),
                Waste = createRecipeComponentDTO.Waste.Map()
            };
        }

        public static RecipeComponent Map(this UpdateRecipeComponentDTO updateRecipeComponentDTO)
        {
            return new RecipeComponent
            {
                Id = updateRecipeComponentDTO.Id,
                Ingredient = new Ingredient { ItemId = updateRecipeComponentDTO.IngredientId },
                Waste = updateRecipeComponentDTO.Waste.Map(),
                Weight = updateRecipeComponentDTO.Weight.Map(),
            };
        }
        public static IEnumerable<RecipeComponentDTO> Map(this IEnumerable<RecipeComponent> recipeComponents)
        {
            return recipeComponents.Select(Map);
        }

        public static IEnumerable<RecipeComponent> Map(this IEnumerable<RecipeComponentDTO> recipeComponentDTOs)
        {
            return recipeComponentDTOs.Select(Map);
        }

        public static IEnumerable<RecipeComponent> Map(this IEnumerable<CreateRecipeComponentDTO> createRecipeComponentDTOs)
        {
            return createRecipeComponentDTOs.Select(Map);
        }
        public static IEnumerable<RecipeComponent> Map(this IEnumerable<UpdateRecipeComponentDTO> updateRecipeComponentDTOs)
        {
            return updateRecipeComponentDTOs.Select(Map);
        }
    }
}
