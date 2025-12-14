using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes.Components;

namespace MySaaS.Application.DTOs.Production.Recipes
{
    public class CreateRecipeDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required CreateRecipeInfoDTO RecipeInfo { get; set; }
    }
}
