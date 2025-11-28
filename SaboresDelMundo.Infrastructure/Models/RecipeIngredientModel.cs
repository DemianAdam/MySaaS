using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class RecipeIngredientModel
    {
        public required int RecipeId { get; set; }

        //Ingredient - Supply
        public required int Supply_Id { get; set; }
        public required string Supply_Name { get; set; }
        public string? Supply_Description { get; set; }

        //Ingredient - Recipe 
        public int? Ingredient_Recipe_Id { get; set; }
        public string? Ingredient_Recipe_Name { get; set; }

        //Ingredient - Recipe - Quantity    
        public int? Ingredient_Recipe_Quantity_UnitId { get; set; }
        public double? Ingredient_Recipe_Quantity_Amount { get; set; }
        public string? Ingredient_Recipe_Quantity_Unit_Name { get; set; }

        //Weight and Waste
        public required int Weight_UnitId { get; set; }
        public required double Weight_Amount { get; set; }
        public required string Weight_Unit_Name { get; set; }

        public required int Waste_UnitId { get; set; }
        public required double Waste_Amount { get; set; }
        public required string Waste_Unit_Name { get; set; }

        public bool HasRecipe()
        {
            return Ingredient_Recipe_Id is not null &&
                   Ingredient_Recipe_Name is not null &&
                   Ingredient_Recipe_Quantity_UnitId is not null &&
                   Ingredient_Recipe_Quantity_Amount is not null &&
                   Ingredient_Recipe_Quantity_Unit_Name is not null;
        }
    }
}
