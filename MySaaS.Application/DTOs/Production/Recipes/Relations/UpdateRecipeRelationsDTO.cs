using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes.Components;

namespace MySaaS.Application.DTOs.Production.Recipes.Relations
{
    public class UpdateRecipeRelationsDTO
    {
        public required List<UpdateRecipeComponentDTO> Ingredients { get; set; }
        public required CreateQuantityDTO Quantity { get; set; }
    }
}
