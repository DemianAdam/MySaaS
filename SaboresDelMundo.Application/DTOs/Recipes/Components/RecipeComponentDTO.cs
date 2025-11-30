using MySaaS.Application.DTOs.Common;
using MySaaS.Application.DTOs.Items.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.DTOs.Recipes.Components
{
    public class RecipeComponentDTO
    {
        public required IngredientDTO Ingredient { get; set; }
        public required QuantityDTO Weight { get; set; }
        public required QuantityDTO Waste { get; set; }
    }
}
