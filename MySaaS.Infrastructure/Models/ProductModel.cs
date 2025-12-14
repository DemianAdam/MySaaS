using MySaaS.Domain.Entities.Production.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class ProductModel
    {
        public required int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public required decimal Price { get; set; }
        public int? RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? RecipeDescription { get; set; }
        public decimal? RecipeAmount { get; set; }
        public int? RecipeUnitId { get; set; }
        public string? RecipeUnitName { get; set; }

        public bool HasRecipe()
        {
            return RecipeId is not null &&
                RecipeName is not null &&
                RecipeDescription is not null &&
                RecipeAmount is not null &&
                RecipeUnitId is not null &&
                RecipeUnitName is not null;
        }
    }
}
