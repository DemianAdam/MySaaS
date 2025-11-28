using MySaaS.Application.DTOs.Common;
using MySaaS.Application.DTOs.Recipes.Components;

namespace MySaaS.Application.DTOs.Recipes
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<RecipeComponentDTO> Ingredients { get; set; } = new();
        public required QuantityDTO Quantity { get; set; }
    }
}
