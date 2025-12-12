using MySaaS.Application.DTOs.Production.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Products
{
    public class ProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public RecipeDTO? Recipe { get; set; }
    }
}
