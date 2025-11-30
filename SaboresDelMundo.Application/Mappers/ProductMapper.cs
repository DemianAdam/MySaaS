using MySaaS.Application.DTOs.Items.Products;
using MySaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Mappers
{
    public static class ProductMapper
    {
        public static Product Map(this ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Recipe = productDTO.Recipe?.Map()
            };
        }
        public static ProductDTO Map(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Recipe = product.Recipe?.Map()
            };
        }

        public static IEnumerable<Product> Map(this IEnumerable<ProductDTO> productDTOs)
        {
            return productDTOs.Select(productDTO => productDTO.Map());
        }

        public static IEnumerable<ProductDTO> Map(this IEnumerable<Product> products)
        {
            return products.Select(product => product.Map());
        }

        public static Product Map(this CreateProductDTO createProductDTO)
        {
            return new Product
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Price = createProductDTO.Price,
                Recipe = createProductDTO.Recipe?.Map()
            };
        }

        public static Product Map(this UpdateProductDTO updateProductDTO)
        {
            return new Product
            {
                Id = updateProductDTO.Id,
                Name = updateProductDTO.Name,
                Description = updateProductDTO.Description,
                Price = updateProductDTO.Price,
                Recipe = updateProductDTO.Recipe?.Map()
            };
        }

    }
}
