using MySaaS.Application.DTOs.Common.Quantity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Recipes.Components
{
    public class CreateRecipeComponentDTO
    {
        public required int IngredientId { get; set; }
        public required CreateQuantityDTO Weight { get; set; }
        public required CreateQuantityDTO Waste { get; set; }
    }
}
