using MySaaS.Application.DTOs.Products;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Mappers
{
    public static class ProductMapper
    {
        public static Product Map(this ProductDTO productDTO)
        {
            Item item = new Item
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description
            };

            return new Product
            {
                ItemId = item.Id,
                Item = item,
                Price = productDTO.Price,
                Recipe = productDTO.Recipe?.Map()
            };
        }
        public static ProductDTO Map(this Product product)
        {
            if(product.Item is null)
            {
                throw new ArgumentNullException(nameof(product.Item), "Product's Item property cannot be null when mapping to ProductDTO.");
            }

            return new ProductDTO
            {
                Id = product.ItemId,
                Name = product.Item.Name,
                Description = product.Item.Description,
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
            Item item = new Item
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description
            };

            //TODO: Fix
            //return new Product(createProductDTO.Categories?.Map())
            return new Product()
            {
                ItemId = item.Id,
                Item = item,
                Price = createProductDTO.Price,
                RecipeId = createProductDTO.RecipeId,
            };
        }

        public static Product Map(this UpdateProductDTO updateProductDTO)
        {
            Item item = new Item
            {
                Id = updateProductDTO.Id,
                Name = updateProductDTO.Name,
                Description = updateProductDTO.Description
            };
            //TODO: Fix
            //return new Product(updateProductDTO.Categories?.Map())
            return new Product()
            {
                ItemId = item.Id,
                Item = item,
                Price = updateProductDTO.Price,
                RecipeId = updateProductDTO.RecipeId
            };
        }

    }
}
