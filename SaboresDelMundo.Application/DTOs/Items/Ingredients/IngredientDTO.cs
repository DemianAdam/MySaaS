using MySaaS.Application.DTOs.Recipes;

namespace MySaaS.Application.DTOs.Items.Ingredients
{
    public class IngredientDTO : ItemDTO
    {
        public RecipeDTO? Recipe { get; set; }
    }
}
