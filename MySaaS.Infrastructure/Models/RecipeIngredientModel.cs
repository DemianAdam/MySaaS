using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class RecipeIngredientModel
    {
        public required int Id { get; set; }
        //Recipe info
        public required int RecipeItemId { get; set; }
        public required string RecipeItemName { get; set; }
        public string? RecipeItemDescription { get; set; }

        //Ingredient info
        public required int IngredientItemId { get; set; }
        public required string IngredientItemName { get; set; }
        public string? IngredientItemDescription { get; set; }

        //Recipe info of the ingredient (if it has one)
        public int? Ingredient_RecipeItemId { get; set; }
        public string? Ingredient_RecipeItemName { get; set; }
        public string? Ingredient_RecipeItemDescription { get; set; }
        public decimal? Ingredient_RecipeAmount { get; set; }
        public int? Ingredient_RecipeUnitId { get; set; }
        public string? Ingredient_RecipeUnitName { get; set; }

        //Weight and Waste
        public required int Weight_UnitId { get; set; }
        public required decimal Weight_Amount { get; set; }
        public required string Weight_Unit_Name { get; set; }

        public required int Waste_UnitId { get; set; }
        public required decimal Waste_Amount { get; set; }
        public required string Waste_Unit_Name { get; set; }

        public bool HasRecipe()
        {
            return Ingredient_RecipeItemId is not null &&
                   Ingredient_RecipeItemName is not null &&
                   Ingredient_RecipeItemDescription is not null &&
                   Ingredient_RecipeUnitId is not null &&
                   Ingredient_RecipeAmount is not null &&
                   Ingredient_RecipeUnitName is not null;
        }
    }
}
