using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Production.Recipes.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Recipes
{
    public class UpdateRecipeDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required UpdateRecipeRelationsDTO RecipeInfo { get; set; }
    }
}
