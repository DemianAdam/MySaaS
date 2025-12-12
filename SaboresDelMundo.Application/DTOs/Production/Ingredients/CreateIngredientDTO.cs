using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.DTOs.Production.Recipes.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Ingredients
{
    public class CreateIngredientDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public CreateRecipeInfoDTO? RecipeInfo { get; set; }
    }
}
