using MySaaS.Domain.Entities.Production;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal class RecipeIngredientSQL
    {
        #region Query
        private const string SelectColumns =
                $"""
                    recipe_ingredients.id_recipe AS {nameof(RecipeIngredientModel.RecipeItemId)},
                    recipe_ingredients.id_ingredient AS {nameof(RecipeIngredientModel.IngredientItemId)},
                    items.name AS {nameof(RecipeIngredientModel.IngredientItemName)},
                    items.description AS {nameof(RecipeIngredientModel.IngredientItemDescription)},
                    itemsRecipe.item_id AS {nameof(RecipeIngredientModel.Ingredient_RecipeItemId)},
                    itemsRecipe.name AS {nameof(RecipeIngredientModel.Ingredient_RecipeItemName)},
                    itemsRecipe.description AS {nameof(RecipeIngredientModel.Ingredient_RecipeItemDescription)},
                    recipes.quantity_unit_id AS {nameof(RecipeIngredientModel.Ingredient_RecipeUnitId)},
                    recipes.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                    unities_recipe.name AS {nameof(RecipeIngredientModel.Ingredient_RecipeUnitName)},
                    recipe_ingredients.weight_unit_id AS {nameof(RecipeIngredientModel.Weight_UnitId)},
                    recipe_ingredients.weight_quantity AS {nameof(RecipeIngredientModel.Weight_Amount)},
                    unities_weight.name AS {nameof(RecipeIngredientModel.Weight_Unit_Name)},
                    recipe_ingredients.waste_unit_id AS {nameof(RecipeIngredientModel.Waste_UnitId)},
                    recipe_ingredients.waste_quantity AS {nameof(RecipeIngredientModel.Waste_Amount)},
                    unities_waste.name AS {nameof(RecipeIngredientModel.Waste_Unit_Name)}
                """;


        public const string Select =
            $"""
                SELECT
                    {SelectColumns}
                FROM recipe_ingredients
                LEFT JOIN items ON items.item_id = recipe_ingredients.id_ingredient
                LEFT JOIN ingredients ON ingredients.id_item = items.item_id
                LEFT JOIN recipes ON recipes.recipe_id = ingredients.id_recipe
                LEFT JOIN items AS itemsRecipe ON itemsRecipe.item_id = recipes.recipe_id
                LEFT JOIN unities AS unities_recipe ON unities_recipe.unit_id = recipes.quantity_unit_id
                LEFT JOIN unities AS unities_weight ON unities_weight.unit_id = recipe_ingredients.weight_unit_id
                LEFT JOIN unities AS unities_waste ON unities_waste.unit_id = recipe_ingredients.waste_unit_id
             """;

        public const string SelectById =
            $"""
                {Select}
                WHERE recipe_ingredients.id_recipe = @RecipeId
            """;
        #endregion


        #region Manipulation
        public const string Insert =
            """
                INSERT INTO recipe_ingredients
                (id_recipe, id_ingredient, weight_unit_id, weight_quantity, waste_unit_id, waste_quantity)
                VALUES 
                (@Recipe_Id, @Ingredient_Id, @Weight_Unit_Id, @Weight_Quantity, @Waste_Unit_Id, @Waste_Quantity);
            """;
        #endregion
    }
}
