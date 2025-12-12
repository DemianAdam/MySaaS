using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class ProductSQL
    {
        #region Manipulation
        public const string Insert =
           """
                INSERT INTO products
                    (id_item, price, id_recipe)
                VALUES
                    (@ItemId, @Price, @RecipeId)
            """;
        public const string Delete =
            """
                DELETE FROM products
                WHERE id_item = @Id;
            """;
        public const string Update =
            """
                UPDATE products
                SET
                    price = @Price,
                    id_recipe = @RecipeId
                WHERE id_item = @Id;
            """;
        #endregion
        #region Query
        public const string SelectWithCategories =
            $"""
                {Select};
                {ProductCategorySQL.Select}
            """;
        public const string Select =
            $"""
                SELECT
                    items.item_id AS {nameof(ProductModel.ItemId)},
                    items.name AS {nameof(ProductModel.ItemName)},
                    items.description AS {nameof(ProductModel.ItemDescription)},
                    products.price AS {nameof(ProductModel.Price)},
                    itemsRecipe.item_id AS {nameof(ProductModel.RecipeId)},
                    itemsRecipe.name AS {nameof(ProductModel.RecipeName)},
                    itemsRecipe.description AS {nameof(ProductModel.RecipeDescription)},
                    recipes.quantity AS {nameof(ProductModel.RecipeAmount)},
                    unities.unit_id AS {nameof(ProductModel.RecipeUnitId)},
                    unities.name AS {nameof(ProductModel.RecipeUnitName)}
                FROM products
                LEFT JOIN items ON items.item_id = products.id_item
                LEFT JOIN items AS itemsRecipe ON itemsRecipe.item_id = products.id_recipe
                LEFT JOIN recipes ON recipes.recipe_id = products.id_recipe
                LEFT JOIN unities ON unities.unit_id = recipes.quantity_unit_id
            """;

        public const string SelectById =
            $"""
                {Select}
                WHERE products.id_item = @Id
            """;
        public const string Exists =
            """
                SELECT EXISTS (
                    SELECT 1
                    FROM products
                    WHERE id_item = @Id
                )
            """;
        #endregion
    }
}
