using MySaaS.Application.DTOs.Common.Items;
using MySaaS.Application.DTOs.Production.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Products
{
    public class ProductDTO : ItemDTO
    {
        public required decimal Price { get; set; }
        public RecipeDTO? Recipe { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
