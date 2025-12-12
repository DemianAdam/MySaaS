using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Domain.Entities.Products;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class ProductMapper
    {
        public static Product Map(ProductModel productModel)
        {
            Item item = new Item()
            {
                Id = productModel.ItemId,
                Name = productModel.ItemName,
                Description = productModel.ItemDescription,
            };
            Recipe? recipe = GetRecipe(productModel);

            return new Product
            {
                ItemId = item.Id,
                Item = item,
                Price = productModel.Price,
                RecipeId = productModel.RecipeId,
                Recipe = recipe,
            };
        }

        private static Recipe? GetRecipe(ProductModel productModel)
        {
            if (!productModel.HasRecipe())
            {
                return null;
            }

            return new RecipeModel()
            {
                ItemId = productModel.RecipeId!.Value,
                RecipeAmount = productModel.RecipeAmount!.Value,
                ItemName = productModel.RecipeName!,
                ItemDescription = productModel.RecipeDescription,
                UnitId = productModel.RecipeUnitId!.Value,
                UnitName = productModel.RecipeUnitName!,
            }.Map();
        }

        public static IEnumerable<Product> Map(this IEnumerable<ProductModel> products)
        {
            return products.Select(x => Map(x));
        }
    }
}
