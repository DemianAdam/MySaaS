using MySaaS.Application.DTOs.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Products
{
    public class UpdateProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public UpdateRecipeDTO? Recipe { get; set; }
    }
}
