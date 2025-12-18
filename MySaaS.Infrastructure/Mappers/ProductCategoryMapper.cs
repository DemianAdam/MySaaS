using MySaaS.Domain.Entities.Products;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class ProductCategoryMapper
    {
        public static ProductComponent Map(this ProductCategoryModel productCategoryModel)
        {
            Category category = new Category()
            {
                Id = productCategoryModel.CategoryId,
                Name = productCategoryModel.CategoryName
            };
            return new ProductComponent()
            {
                Id = productCategoryModel.Id,
                CategoryId = category.Id,
                Category = category
            };
        }

        public static IEnumerable<ProductComponent> Map(this IEnumerable<ProductCategoryModel> productCategoryModels)
        {
            return productCategoryModels.Select(x => x.Map());
        }
    }
}
