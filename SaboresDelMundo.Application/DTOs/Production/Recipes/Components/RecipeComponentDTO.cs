using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.DTOs.Production.Recipes.Components
{
    public class RecipeComponentDTO
    {
        public required IngredientDTO Ingredient { get; set; }
        public required QuantityDTO Weight { get; set; }
        public required QuantityDTO Waste { get; set; }
    }
}
