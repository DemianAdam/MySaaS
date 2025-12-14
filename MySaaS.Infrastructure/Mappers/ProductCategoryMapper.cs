using MySaaS.Domain.Entities.Products;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class ProductCategoryMapper
    {
        public static Category Map(this ProductCategoryModel productCategoryModel)
        {
            return new Category()
            {
                Id = productCategoryModel.CategoryId,
                Name = productCategoryModel.CategoryName
            };
        }

        public static IEnumerable<Category> Map(this IEnumerable<ProductCategoryModel> productCategoryModels)
        {
            return productCategoryModels.Select(x => x.Map());
        }
    }
}
