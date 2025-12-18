using MySaaS.Application.DTOs.Production.Ingredients;
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
    public static class IngredientMapper
    {
        public static IngredientDTO Map(this Ingredient ingredient)
        {
            if (ingredient.Item is null)
            {
                throw new ArgumentNullException(nameof(ingredient.Item), "Item property cannot be null when mapping Ingredient to IngredientDTO.");
            }

            return new IngredientDTO
            {
                Id = ingredient.ItemId,
                Name = ingredient.Item.Name,
                Description = ingredient.Item.Description,
                Recipe = ingredient.Recipe?.Map()
            };
        }

        public static Ingredient Map(this IngredientDTO ingredientDTO)
        {
            Item item = new Item
            {
                Id = ingredientDTO.Id,
                Name = ingredientDTO.Name,
                Description = ingredientDTO.Description
            };

            return new Ingredient
            {
                ItemId = item.Id,
                Item = item,
                Recipe = ingredientDTO.Recipe?.Map()
            };
        }

        public static Ingredient Map(this UpdateIngredientDTO updateIngredientDTO)
        {
            Item item = new Item
            {
                Id = updateIngredientDTO.Id,
                Name = updateIngredientDTO.Name,
                Description = updateIngredientDTO.Description
            };
            return new Ingredient
            {
                ItemId = item.Id,
                Item = item,
                Recipe = updateIngredientDTO.Recipe?.Map()
            };
        }

        public static Ingredient Map(this CreateIngredientDTO createIngredientDTO)
        {
            Item item = new Item
            {
                Name = createIngredientDTO.Name,
                Description = createIngredientDTO.Description
            };

            Recipe? recipe = null;

            if (createIngredientDTO.RecipeInfo is not null)
            {
                recipe = new Recipe(createIngredientDTO.RecipeInfo.Ingredients.Map())
                {
                    Id = item.Id,
                    Item = item,
                    Quantity = createIngredientDTO.RecipeInfo.Quantity.Map()
                };
            }

            return new Ingredient
            {
                ItemId = item.Id,
                Item = item,
                Recipe = recipe
            };
        }

        public static IngredientDTO Map(this CreateIngredientDTO createIngredientDTO, int id, RecipeDTO? recipe)
        {
            return new IngredientDTO
            {
                Id = id,
                Name = createIngredientDTO.Name,
                Description = createIngredientDTO.Description,
                Recipe = recipe
            };
        }

        public static IngredientResponse ToResponse(this Ingredient ingredient)
        {
            if (ingredient.Item is null)
            {
                throw new ArgumentNullException(nameof(ingredient.Item), "Item property cannot be null when mapping Ingredient to IngredientResponse.");
            }

            return new IngredientResponse
            {
                Id = ingredient.ItemId,
                Name = ingredient.Item.Name,
                Description = ingredient.Item.Description
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
