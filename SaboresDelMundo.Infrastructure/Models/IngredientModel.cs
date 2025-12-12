using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class IngredientModel
    {
        public required int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }

        public int? RecipeItemId { get; set; }
        public string? RecipeItemName { get; set; }
        public string? RecipeItemDescription { get; set; }
        public double? RecipeAmount { get; set; }
        public int? RecipeUnitId { get; set; }
        public string? RecipeUnitName { get; set; }

        public bool HasRecipe()
        {
            return RecipeItemId is not null &&
                RecipeItemName is not null &&
                RecipeItemDescription is not null &&
                RecipeAmount is not null &&
                RecipeUnitId is not null &&
                RecipeUnitName is not null;
        }
    }
}
