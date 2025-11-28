using MySaaS.Application.DTOs.Recipes;
using MySaaS.Application.DTOs.Supplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.DTOs.Ingredients
{
    public class IngredientDTO : SupplyDTO
    {
        public RecipeDTO? Recipe { get; set; }
    }
}
