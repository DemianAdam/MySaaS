using MySaaS.Domain.Entities.Production.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class RecipeSQL
    {
        public const string Select =
            $"""
                SELECT
                    items.item_id AS {nameof(RecipeModel.ItemId)},
                    items.name AS {nameof(RecipeModel.ItemName)},
                    items.description AS {nameof(RecipeModel.ItemDescription)},
                    recipes.quantity AS {nameof(RecipeModel.RecipeAmount)},
                    unities.unit_id AS {nameof(RecipeModel.UnitId)},
                    unities.name AS {nameof(RecipeModel.UnitName)}
                FROM recipes
                LEFT JOIN items ON items.item_id = recipes.recipe_id
                LEFT JOIN unities ON unities.unit_id = recipes.quantity_unit_id
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE recipes.recipe_id = @RecipeId
            """;

        #region Query
        public const string SelectWithIngredients =
            $"""
                {Select};

                {RecipeIngredientSQL.Select}
            """;
        public const string SelectWithIngredientsById =
            $"""
                {SelectById};
                {RecipeIngredientSQL.SelectById};
            """;
        public const string Exists =
            """
                SELECT EXISTS (
                    SELECT 1
                    FROM recipes
                    WHERE recipe_id = @Id
                );
            """;
        #endregion
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO recipes
                (recipe_id, quantity_unit_id, quantity)
                VALUES 
                (@RecipeId, @Quantity_Unit_Id, @Quantity_Unit)
                RETURNING recipe_id;
            """;
        public const string Delete =
            """
                DELETE FROM recipes
                WHERE recipe_id = @RecipeId
            """;
        public const string Update =
            """
                UPDATE recipes
                SET
                    quantity_unit_id = @Quantity_Unit_Id,
                    quantity = @Quantity_Unit
                WHERE recipe_id = @RecipeId;
            """;
        #endregion
    }
}
