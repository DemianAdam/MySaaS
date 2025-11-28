using MySaaS.Application.DTOs.Ingredients;
using MySaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Mappers
{
    internal static class IngredientMapper
    {
        public static IngredientDTO Map(this Ingredient ingredient)
        {
            if (ingredient.Supply is null)
            {
                throw new ArgumentNullException(nameof(ingredient.Supply), "Supply property cannot be null when mapping Ingredient to IngredientDTO.");
            }

            return new IngredientDTO
            {
                Id = ingredient.Supply.Id,
                Name = ingredient.Supply.Name,
                Description = ingredient.Supply.Description,
                Recipe = ingredient.Recipe?.Map()
            };
        }

        public static Ingredient Map(this IngredientDTO ingredientDTO)
        {
            Supply supply = new Supply
            {
                Id = ingredientDTO.Id,
                Name = ingredientDTO.Name,
                Description = ingredientDTO.Description
            };

            return new Ingredient
            {
                SupplyId = supply.Id,
                Supply = supply,
                Recipe = ingredientDTO.Recipe?.Map()
            };
        }

        public static Ingredient Map(this UpdateIngredientDTO updateIngredientDTO)
        {
            Supply supply = new Supply
            {
                Id = updateIngredientDTO.Id,
                Name = updateIngredientDTO.Name,
                Description = updateIngredientDTO.Description
            };
            return new Ingredient
            {
                SupplyId = supply.Id,
                Supply = supply,
                Recipe = updateIngredientDTO.Recipe?.Map()
            };
        }

        public static Ingredient Map(this CreateIngredientDTO createIngredientDTO)
        {
            Supply supply = new Supply
            {
                Name = createIngredientDTO.Name,
                Description = createIngredientDTO.Description
            };

            return new Ingredient
            {
                SupplyId = supply.Id,
                Supply = supply,
                Recipe = createIngredientDTO.Recipe?.Map()
            };
        }

        public static IEnumerable<IngredientDTO> Map(this IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Select(ingredient => ingredient.Map());
        }

        public static IEnumerable<Ingredient> Map(this IEnumerable<IngredientDTO> ingredientDTOs)
        {
            
            return ingredientDTOs.Select(ingredientDTO => ingredientDTO.Map());
        }

    }

    

    
    
}
