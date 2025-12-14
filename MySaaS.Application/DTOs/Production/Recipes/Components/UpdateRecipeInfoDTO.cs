using MySaaS.Application.DTOs.Common.Quantity;

namespace MySaaS.Application.DTOs.Production.Recipes.Components
{
    public class UpdateRecipeInfoDTO
    {
        public required List<UpdateRecipeComponentDTO> Ingredients { get; set; }
        public required CreateQuantityDTO Quantity { get; set; }
    }
}
