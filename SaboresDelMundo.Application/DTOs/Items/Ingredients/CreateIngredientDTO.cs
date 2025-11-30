using MySaaS.Application.DTOs.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Items.Ingredients
{
    public class CreateIngredientDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public CreateRecipeDTO? Recipe { get; set; }
    }
}
