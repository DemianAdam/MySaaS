using MySaaS.Domain.Entities.Production;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class IngredientSQL
    {
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO Ingredients 
                    (id_item, id_recipe)
                VALUES
                    (@ItemId, @RecipeId);
            """;
        public const string Delete =
            """
                DELETE FROM ingredients 
                WHERE id_item = @Id
            """;

        public const string Update =
            """
                UPDATE ingredients
                SET id_recipe = @RecipeId
                WHERE id_item = @ItemId
            """;
        #endregion

        #region Query
        public const string Select =
            $"""
                SELECT 
                    items.item_id AS {nameof(IngredientModel.ItemId)},
                    items.name AS {nameof(IngredientModel.ItemName)},
                    items.description AS {nameof(IngredientModel.ItemDescription)},
                    itemsRecipe.item_id AS {nameof(IngredientModel.RecipeItemId)},
                    itemsRecipe.name AS {nameof(IngredientModel.RecipeItemName)},
                    itemsRecipe.description AS {nameof(IngredientModel.RecipeItemDescription)},
                    recipes.quantity AS {nameof(IngredientModel.RecipeAmount)},
                    unities.unit_id AS {nameof(IngredientModel.RecipeUnitId)},
                    unities.name AS {nameof(IngredientModel.RecipeUnitName)}
                FROM ingredients
                LEFT JOIN items ON items.item_id = ingredients.id_item
                LEFT JOIN items AS itemsRecipe ON itemsRecipe.item_id = ingredients.id_recipe
                LEFT JOIN recipes ON recipes.recipe_id = ingredients.id_recipe
                LEFT JOIN unities ON unities.unit_id = recipes.quantity_unit_id
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE ingredients.id_item = @Id
            """;

        public const string SelectByIdWithIngredient =
            $"""
                {SelectById};

                {RecipeIngredientSQL.Select}
                   WHERE recipe_ingredients.id_recipe = (
                       SELECT ingredients.id_recipe
                       FROM ingredients
                       WHERE ingredients.id_item = @Id
                       );
            """;
        public const string Exists =
            """
                SELECT EXISTS (
                    SELECT 1
                    FROM ingredients
                    WHERE id_item = @ItemId
                );
            """;
        #endregion
    }
}
