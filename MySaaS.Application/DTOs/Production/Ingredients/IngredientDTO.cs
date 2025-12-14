using MySaaS.Application.DTOs.Common.Items;
using MySaaS.Application.DTOs.Production.Recipes;

namespace MySaaS.Application.DTOs.Production.Ingredients
{
    public class IngredientDTO : ItemDTO
    {
        public RecipeDTO? Recipe { get; set; }
    }
}
