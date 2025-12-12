using MySaaS.Application.DTOs.Common.Items;
using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes.Components;

namespace MySaaS.Application.DTOs.Production.Recipes
{
    public class RecipeDTO : ItemDTO
    {
        public List<RecipeComponentDTO> Ingredients { get; set; } = new();
        public required QuantityDTO Quantity { get; set; }
    }
}
