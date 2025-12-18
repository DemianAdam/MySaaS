using MySaaS.Application.DTOs.Common.Quantity;

namespace MySaaS.Application.DTOs.Production.Recipes.Relations
{
    public class RecipeRelationsResponse
    {
        public required int IngredientId { get; set; }
        public required QuantityResponse Weight { get; set; }
        public required QuantityResponse Waste { get; set; }
    }
}
