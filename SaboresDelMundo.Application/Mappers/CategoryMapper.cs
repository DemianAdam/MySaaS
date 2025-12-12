using MySaaS.Application.DTOs.Products.Category;
using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Mappers
{
    public static class CategoryMapper
    {
        public static Category Map(this CategoryDTO categoryDTO)
        {
            return new Category()
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
            };
        }
        public static Category Map(this CreateCategoryDTO createCategoryDTO)
        {
            return new Category()
            {
                Name = createCategoryDTO.Name,
            };
        }

        public static IEnumerable<Category> Map(this IEnumerable<CategoryDTO> categoryDTOs)
        {
            return categoryDTOs.Select(x => x.Map());
        }
    }
}
