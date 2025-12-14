using MySaaS.Application.DTOs.Common.Quantity;

namespace MySaaS.Application.DTOs.Production.Recipes.Components
{
    public class UpdateRecipeComponentDTO
    {
        public required int Id { get; set; }
        public required int IngredientId { get; set; }
        public required CreateQuantityDTO Weight { get; set; }
        public required CreateQuantityDTO Waste { get; set; }
    }
}
