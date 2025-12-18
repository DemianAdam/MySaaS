using MySaaS.Application.DTOs.Common.Items;
using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes.Components;

namespace MySaaS.Application.DTOs.Production.Recipes
{
    public class RecipeDTO : ItemDTO
    {
        public required List<RecipeComponentDTO> Ingredients { get; set; }
        public required QuantityDTO Quantity { get; set; }
    }
}
