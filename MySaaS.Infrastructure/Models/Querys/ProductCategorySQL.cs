using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class ProductCategorySQL
    {
        public const string Select =
                $"""
                    SELECT
                        product_categories_link.id AS {nameof(ProductCategoryModel.Id)},
                        product_categories.id AS {nameof(ProductCategoryModel.CategoryId)},
                        product_categories.name AS {nameof(ProductCategoryModel.CategoryName)},
                        product_categories_link.product_id AS {nameof(ProductCategoryModel.ProductId)}
                    FROM product_categories_link
                    LEFT JOIN product_categories ON product_categories.id = product_categories_link.product_category_id
                """;
    }
}
