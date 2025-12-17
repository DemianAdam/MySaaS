using MySaaS.Application.DTOs.Products.Components;
using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Mappers
{
    public static class ProductComponentMapper
    {
        public static ProductComponent Map(this CreateProductComponentDTO createProductComponentDTO)
        {
            return new ProductComponent
            {
                CategoryId = createProductComponentDTO.CategoryId
            };
        }
        public static ProductComponent Map(this UpdateProductComponentDTO updateProductComponentDTO)
        {
            return new ProductComponent
            {
                Id = updateProductComponentDTO.Id,
                CategoryId = updateProductComponentDTO.CategoryId
            };
        }
        public static ProductComponent Map(this ProductComponentDTO productComponentDTO)
        {
            return new ProductComponent
            {
                Id = productComponentDTO.Id,
                CategoryId = productComponentDTO.CategoryId,
                Category = productComponentDTO.Category.Map()
            };
        }
        public static ProductComponentDTO Map(this ProductComponent productComponent)
        {
            if(productComponent.Category is null)
            {
                throw new ArgumentNullException(nameof(productComponent.Category), "Category property cannot be null when mapping to ProductComponentDTO.");
            }

            return new ProductComponentDTO
            {
                Id = productComponent.Id,
                CategoryId = productComponent.CategoryId,
                Category = productComponent.Category.Map()
            };
        }

        public static IEnumerable<ProductComponentDTO> Map(this IEnumerable<ProductComponent> productComponents)
        {
            return productComponents.Select(Map);
        }
        public static IEnumerable<ProductComponent> Map(this IEnumerable<ProductComponentDTO> productComponentDTOs)
        {
            return productComponentDTOs.Select(Map);
        }   
        public static IEnumerable<ProductComponent> Map(this IEnumerable<CreateProductComponentDTO> createProductComponentDTOs)
        {
            return createProductComponentDTOs.Select(Map);
        }
        public static IEnumerable<ProductComponent> Map(this IEnumerable<UpdateProductComponentDTO> updateProductComponentDTOs)
        {
            return updateProductComponentDTOs.Select(Map);
        }
    }
}
